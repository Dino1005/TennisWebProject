import { React, useEffect, useState } from "react"
import { getReservationsByMemberIdAsync } from "../services/ReservationService"
import { useNavigate } from "react-router-dom"
import { useAuth } from "../contexts/AppContext"
import {
  extractDate,
  extractHours,
  getDayOfWeek,
} from "../services/DateService"

export default function UserReservation() {
  const [reservations, setReservations] = useState([])
  const { userId, setDate } = useAuth()
  const navigate = useNavigate()

  useEffect(() => {
    if (userId) {
      getReservationsByMemberIdAsync(100, 1, userId).then((res) => {
        setReservations(res.Items)
      })
    }
  }, [userId])

  function groupReservationsByDay() {
    const groupedReservations = {}

    reservations.forEach((reservation) => {
      if (new Date(reservation.Time).getDate() >= new Date().getDate()) {
        const dateKey = extractDate(reservation.Time)
        if (!groupedReservations[dateKey]) {
          groupedReservations[dateKey] = []
        }
        groupedReservations[dateKey].push(reservation)
      }
    })

    return groupedReservations
  }

  function renderGroupedReservations() {
    const groupedReservations = groupReservationsByDay()
    return Object.entries(groupedReservations).map(
      ([dateKey, reservations]) => (
        <div className="reservation-day" key={dateKey}>
          <h2 className="time">{`${getDayOfWeek(dateKey)}, ${dateKey}`}</h2>
          <div className="reservation-card-container">
            {reservations.map((reservation) => (
              <div
                className="reservation-card"
                key={reservation.Id}
                onClick={() => {
                  setDate(new Date(reservation.Time).toISOString().slice(0, 10))
                  navigate(`/court/${reservation.Court.Id}`)
                }}>
                <p className="reservation-court-name">
                  {reservation.Court.Name}
                </p>
                <p className="reservation-time">
                  {extractHours(reservation.Time)}
                </p>
              </div>
            ))}
          </div>
        </div>
      )
    )
  }

  return (
    <div className="d-flex align-items-center justify-content-center mt-3">
      <div className="user-reservation-content d-flex flex-column">
        <h2 className="user-reservation-title">Vaše rezervacije</h2>
        <div className="user-reservations mt-4">
          {reservations.length > 0 ? (
            renderGroupedReservations()
          ) : (
            <p className="no-reservations">Nemate nadolazećih rezervacija</p>
          )}
        </div>
      </div>
    </div>
  )
}
