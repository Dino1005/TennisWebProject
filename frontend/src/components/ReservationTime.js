import React from "react"
import { useAuth } from "../contexts/AppContext"
import { getHour } from "../services/DateService"

export default function ReservationTime({ reservations }) {
  const timeSlots = [8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21]
  const { userId } = useAuth()

  return (
    <div className="reservation-hours d-flex align-items-center">
      {timeSlots.map((hour) => {
        const matchingReservation = reservations.find(
          (reservation) => getHour(reservation.Time) === hour
        )
        const isReserved = !!matchingReservation

        const backgroundColor = isReserved
          ? matchingReservation.MemberId === userId
            ? "#5cb85c"
            : "var(--primary-dark)"
          : "var(--secondary-dark)"

        return (
          <div
            key={hour + 1}
            className="reservation-item d-flex align-items-center justify-content-center flex-column">
            <p key={hour - 1}>{hour}</p>
            <div
              key={hour}
              className="reservation-hour"
              style={{
                backgroundColor: backgroundColor,
              }}></div>
          </div>
        )
      })}
    </div>
  )
}
