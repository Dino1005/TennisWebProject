import React from "react"
import { useNavigate } from "react-router"

export default function PageNav() {
  const navigate = useNavigate()

  return (
    <div className="pagenav-container d-flex align-items-center justify-content-center">
      <div className="pagenav-content d-flex align-items-center">
        <div className="pagenav-card" onClick={() => navigate("/reservation")}>
          <img
            src={require("../assets/reservation.png")}
            alt="Reservation"></img>
          <h2>Rezervacija</h2>
        </div>
        <div className="pagenav-card" onClick={() => navigate("/member")}>
          <img src={require("../assets/member.png")} alt="Member"></img>
          <h2>Članovi</h2>
        </div>
        <div className="pagenav-card" onClick={() => navigate("/about")}>
          <img src={require("../assets/logo.png")} alt="About"></img>
          <h2>O klubu</h2>
        </div>
      </div>
    </div>
  )
}
