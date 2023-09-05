import React, { useEffect, useState } from "react"
import { useAuth } from "../contexts/AppContext"
import { getCourtsAsync } from "../services/CourtService"
import { getReservationsByTimeAndCourtIdAsync } from "../services/ReservationService"
import ReservationTime from "./ReservationTime"
import { useNavigate } from "react-router-dom"

export default function CourtTable() {
  const { date } = useAuth()
  const [courts, setCourts] = useState([])
  const navigate = useNavigate()

  useEffect(() => {
    getCourtsAsync(100, 1).then(async (courts) => {
      const updatedCourts = await Promise.all(
        courts.Items.map(async (court) => {
          const reservations = await getReservationsByTimeAndCourtIdAsync(
            100,
            1,
            date,
            court.Id
          )
          return { ...court, Reservations: reservations.Items }
        })
      )
      setCourts(updatedCourts)
    })
  }, [date])

  return (
    <div className="d-flex align-items-center justify-content-center mb-5">
      <div className="court-table">
        {courts.length > 0 ? (
          courts.map((court) => (
            <div
              className="court-card"
              key={court.Id}
              onClick={() => navigate(`/court/${court.Id}`)}>
              <h2>{court.Name}</h2>
              <hr />
              <ReservationTime reservations={court.Reservations} />
              <div className="court-card-footer">
                <p>Rezerviraj</p>
              </div>
            </div>
          ))
        ) : (
          <p className="loading-message">Loading...</p>
        )}
      </div>
    </div>
  )
}
