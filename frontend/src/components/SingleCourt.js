import React, { useEffect } from "react"
import { useAuth } from "../contexts/AppContext"
import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { getReservationsByTimeAndCourtIdAsync } from "../services/ReservationService"
import { Button } from "react-bootstrap"
import { createReservationAsync } from "../services/ReservationService"
import { getHour } from "../services/DateService"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faXmark } from "@fortawesome/free-solid-svg-icons"
import { confirmAlert } from "react-confirm-alert"
import "react-confirm-alert/src/react-confirm-alert.css"
import { toggleReservationAsync } from "../services/ReservationService"

export default function SingleCourt({ court }) {
  const { date, userId } = useAuth()
  const timeSlots = [8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21]
  const [reservations, setReservations] = useState([])
  const [selectedReservations, setSelectedReservations] = useState([])
  const navigate = useNavigate()

  useEffect(() => {
    getReservations()
  }, [court.Id, date])

  async function getReservations() {
    getReservationsByTimeAndCourtIdAsync(100, 1, date, court.Id).then(
      (reservations) => setReservations(reservations.Items)
    )
  }

  function toggleReservation(hour) {
    if (selectedReservations.includes(hour)) {
      setSelectedReservations(
        selectedReservations.filter((reservation) => reservation !== hour)
      )
    } else {
      setSelectedReservations([...selectedReservations, hour])
    }
  }

  async function createReservation() {
    if (selectedReservations.length === 0) {
      return
    }

    const reservationCreationPromises = selectedReservations.map(
      async (hour) => {
        const selectedTime = new Date(date)
        selectedTime.setHours(hour)

        const reservation = {
          CourtId: court.Id,
          Time: selectedTime.toISOString(),
          MemberId: userId,
        }

        return createReservationAsync(reservation)
      }
    )

    try {
      await Promise.all(reservationCreationPromises)

      navigate("/reservation")
    } catch (error) {
      console.error("Error:", error)
    }
  }

  function isPastTime(hour) {
    const reservationDate = new Date(date)
    const currentDate = new Date()

    if (
      reservationDate.getDate() === currentDate.getDate() &&
      reservationDate.getMonth() === currentDate.getMonth() &&
      reservationDate.getFullYear() === currentDate.getFullYear()
    )
      return hour - 1 < currentDate.getHours()
    else if (reservationDate < currentDate) return true
    else return false
  }

  async function handleCancel(id) {
    try {
      confirmAlert({
        title: "Jeste li sigurni želite otkazati rezervaciju?",
        buttons: [
          {
            label: "Da",
            onClick: async () => {
              await toggleReservationAsync(id)
              getReservations()
            },
          },
          {
            label: "Ne",
          },
        ],
      })
    } catch (err) {
      console.log(err)
    }
  }

  return (
    <div className="d-flex align-items-center justify-content-center flex-column mb-5">
      <span className="reserve-btn">
        {!isPastTime(23) ? (
          <Button
            onClick={() => createReservation()}
            cursor="default"
            className={selectedReservations.length > 0 ? "reserve-active" : ""}>
            Potvrdi rezervaciju
          </Button>
        ) : (
          ""
        )}
      </span>
      {timeSlots.map((hour) => {
        const matchingReservation = reservations.find(
          (reservation) => getHour(reservation.Time) === hour
        )
        const isReserved = !!matchingReservation

        const backgroundColor = isReserved
          ? matchingReservation.MemberId === userId
            ? "#5cb85c"
            : "var(--primary-dark)"
          : selectedReservations.includes(hour)
          ? "#0275d8"
          : "var(--secondary-dark)"

        return (
          <div key={hour + 1} className="member-table mt-3">
            <div
              key={hour}
              className={`court-hour d-flex align-items-center ${
                isReserved ? "" : !isPastTime(hour) ? "not-reserved" : ""
              }`}
              onClick={
                isReserved || isPastTime(hour)
                  ? () => {}
                  : () => toggleReservation(hour)
              }
              style={{
                backgroundColor: backgroundColor,
              }}>
              <p key={hour - 1} className={isReserved ? "light" : ""}>
                {hour}:00 - {hour + 1}:00
              </p>
              <div className="court-hour-text d-flex align-items-center justify-content-">
                <p className={isReserved ? "light" : ""}>
                  {isReserved
                    ? `${matchingReservation.Member.FirstName} ${matchingReservation.Member.LastName}`
                    : isPastTime(hour)
                    ? ""
                    : "Rezerviraj"}
                </p>
                {isReserved &&
                  matchingReservation.MemberId === userId &&
                  !isPastTime(hour) && (
                    <FontAwesomeIcon
                      icon={faXmark}
                      color="var(--primary-dark)"
                      style={{
                        width: "30px",
                        height: "30px",
                        cursor: "pointer",
                        paddingRight: "10px",
                      }}
                      onClick={() => handleCancel(matchingReservation.Id)}
                    />
                  )}
              </div>
            </div>
          </div>
        )
      })}
    </div>
  )
}
