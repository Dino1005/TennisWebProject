import React, { useEffect, useRef, useState } from "react"
import { Form, Button } from "react-bootstrap"
import { useAuth } from "../contexts/AppContext"
import { useNavigate } from "react-router-dom"
import FormContainer from "../components/FormContainer"
import {
  getMemberByIdAsync,
  updateMemberAsync,
} from "../services/MemberService"

export default function Profile() {
  const firstNameRef = useRef()
  const lastNameRef = useRef()
  const emailRef = useRef()
  const passwordRef = useRef()
  const passwordConfirmRef = useRef()
  const { currentUser, updateEmail, updatePassword, userId } = useAuth()
  const [error, setError] = useState("")
  const [loading, setLoading] = useState(false)
  const [member, setMember] = useState({})
  const navigate = useNavigate()

  useEffect(() => {
    if (!userId) return
    getMemberByIdAsync(userId).then((member) => {
      setMember(member)
    })
  }, [userId])

  function handleSubmit(e) {
    e.preventDefault()
    console.log(userId)
    if (passwordRef.current.value !== passwordConfirmRef.current.value)
      return setError("Lozinke se ne podudraju")

    const promises = []
    setLoading(true)
    setError("")

    if (
      firstNameRef.current.value !== member?.FirstName ||
      lastNameRef.current.value !== member?.LastName
    ) {
      promises.push(
        updateMemberAsync(userId, {
          FirstName: firstNameRef.current.value,
          LastName: lastNameRef.current.value,
        })
      )
    }
    if (emailRef.current.value !== currentUser.email) {
      promises.push(updateEmail(emailRef.current.value))
    }
    if (passwordRef.current.value) {
      promises.push(updatePassword(passwordRef.current.value))
    }

    Promise.all(promises)
      .then(() => {
        navigate("/profile")
      })
      .catch(() => {
        setError("Neuspješno ažuriranje profila")
      })
      .finally(() => {
        setLoading(false)
      })
  }

  return (
    <FormContainer formTitle="Ažuriraj profil" error={error}>
      <Form onSubmit={handleSubmit}>
        <Form.Group id="fname">
          <Form.Label>Ime</Form.Label>
          <Form.Control
            type="text"
            ref={firstNameRef}
            required
            defaultValue={member?.FirstName}
          />
        </Form.Group>
        <Form.Group id="lname">
          <Form.Label>Prezime</Form.Label>
          <Form.Control
            type="text"
            ref={lastNameRef}
            required
            defaultValue={member?.LastName}
          />
        </Form.Group>
        <Form.Group id="email">
          <Form.Label>Email</Form.Label>
          <Form.Control
            type="email"
            ref={emailRef}
            required
            defaultValue={currentUser.email}
          />
        </Form.Group>
        <Form.Group id="password">
          <Form.Label>Lozinka</Form.Label>
          <Form.Control
            type="password"
            ref={passwordRef}
            placeholder="Ostavi prazno ako ne želiš promijeniti"
          />
        </Form.Group>
        <Form.Group id="password-confirm">
          <Form.Label>Potvrda lozinke</Form.Label>
          <Form.Control
            type="password"
            ref={passwordConfirmRef}
            placeholder="Ostavi prazno ako ne želiš promijeniti"
          />
        </Form.Group>
        <Button
          disabled={loading}
          className="w-100 mt-3 btn-success"
          type="submit">
          Ažuriraj
        </Button>
      </Form>
    </FormContainer>
  )
}
