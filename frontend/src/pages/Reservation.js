import React, { useEffect } from "react"
import Navbar from "../components/Navbar"
import Header from "../components/Header"
import DateSelect from "../components/DateSelect"
import CourtTable from "../components/CourtTable"
import { useAuth } from "../contexts/AppContext"

export default function Reservation() {
  const { setDate } = useAuth()

  useEffect(() => {
    setDate(new Date().toISOString().slice(0, 10))
  }, [])

  return (
    <div>
      <Navbar />
      <Header title={"Rezerviraj teren"} />
      <DateSelect />
      <CourtTable />
    </div>
  )
}
