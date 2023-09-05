import React, { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import Navbar from "../components/Navbar"
import Header from "../components/Header"
import { getCourtByIdAsync } from "../services/CourtService"
import SingleCourt from "../components/SingleCourt"
import DateSelect from "../components/DateSelect"

export default function Court() {
  const { id } = useParams()
  const navigate = useNavigate()
  const [court, setCourt] = useState({})

  useEffect(() => {
    if (!id) {
      navigate("/")
    }
    getCourtByIdAsync(id).then((court) => setCourt(court))
  }, [id])

  return (
    <div>
      <Navbar />
      <Header title={court.Name} />
      <DateSelect />
      <SingleCourt court={court} />
    </div>
  )
}
