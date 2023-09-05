import React from "react"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import {
  faChevronLeft,
  faChevronRight,
} from "@fortawesome/free-solid-svg-icons"
import Form from "react-bootstrap/Form"
import { useAuth } from "../contexts/AppContext"

export default function DateSelect() {
  const { date, setDate } = useAuth()
  return (
    <div className="date-select-container">
      <div className="date-select">
        <button
          className="btn btn-primary"
          onClick={() => {
            const newDate = new Date(date)
            newDate.setDate(newDate.getDate() - 1)
            setDate(newDate.toISOString().slice(0, 10))
          }}>
          <FontAwesomeIcon
            icon={faChevronLeft}
            style={{ width: "20px", height: "20px" }}
          />
        </button>
        <Form.Control
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
        />
        <button
          className="btn btn-primary"
          onClick={() => {
            const newDate = new Date(date)
            newDate.setDate(newDate.getDate() + 1)
            setDate(newDate.toISOString().slice(0, 10))
          }}>
          <FontAwesomeIcon
            icon={faChevronRight}
            style={{ width: "20px", height: "20px" }}
          />
        </button>
      </div>
    </div>
  )
}
