import React, { useEffect } from "react"
import Navbar from "../components/Navbar"
import Header from "../components/Header"
import Form from "react-bootstrap/Form"
import { useState } from "react"
import { getMembersByNameAsync } from "../services/MemberService"

export default function Member() {
  const [name, setName] = useState("")
  const [members, setMembers] = useState([])
  const [memberCount, setMemberCount] = useState(0)

  useEffect(() => {
    updateMembers()
  }, [name])

  function updateMembers() {
    getMembersByNameAsync(100, 1, name).then((response) => {
      setMembers(response.Items)
      setMemberCount(response.TotalCount)
    })
  }

  function extractDate(fullDate) {
    const date = new Date(fullDate)
    const day = date.getDate()
    const month = date.getMonth() + 1
    const year = date.getFullYear()
    return `${day}. ${month}. ${year}.`
  }

  return (
    <div>
      <Navbar />
      <Header title={"Članovi kluba"} />
      <div className="member-content d-flex align-items-center justify-content-center flex-column">
        <Form.Control
          className="w-50 mt-4"
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Unesi ime člana"
        />
        <span className="mt-4">Ukupno članova: {memberCount}</span>
        <div className="member-table mt-4">
          {members.map((member) => {
            return (
              <div key={member.Id} className="member-container">
                <span>
                  {member.FirstName} {member.LastName}
                </span>
                <span>{extractDate(member.DoB)}</span>
              </div>
            )
          })}
        </div>
      </div>
    </div>
  )
}
