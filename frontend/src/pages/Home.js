import React, { useEffect, useState } from "react"
import Navbar from "../components/Navbar"
import Header from "../components/Header"
import PageNav from "../components/PageNav"
import { useAuth } from "../contexts/AppContext"
import UserReservation from "../components/UserReservation"
import { getMemberByIdAsync } from "../services/MemberService"

export default function Home() {
  const { userId } = useAuth()
  const [member, setMember] = useState({})
  useEffect(() => {
    if (!userId) return
    getMemberByIdAsync(userId).then((member) => {
      setMember(member)
    })
  }, [userId])
  return (
    <div>
      <Navbar />
      <Header title={`Dobrodošli, ${member?.FirstName}`} home={true} />
      <PageNav />
      <UserReservation />
    </div>
  )
}
