import React, { useRef, useState } from "react"
import { Form, Button } from "react-bootstrap"
import { useAuth } from "../contexts/AppContext"
import { Link, useNavigate } from "react-router-dom"
import FormContainer from "../components/FormContainer"
import { createMemberAsync } from "../services/MemberService"

export default function Register() {
  const emailRef = useRef()
  const passwordRef = useRef()
  const passwordConfirmRef = useRef()
  const firstNameRef = useRef()
  const lastNameRef = useRef()
  const doBRef = useRef()
  const { register, logout } = useAuth()
  const [error, setError] = useState("")
  const [loading, setLoading] = useState(false)
  const navigate = useNavigate()

  async function handleSubmit(e) {
    e.preventDefault()

    if (passwordRef.current.value.length < 6)
      return setError("Lozinka mora imati najmanje 6 znakova")

    if (passwordRef.current.value !== passwordConfirmRef.current.value)
      return setError("Lozinke se ne podudaraju")

    if (doBRef.current.value > new Date().toISOString().split("T")[0])
      return setError("Datum rođenja ne može biti u budućnosti")

    setLoading(true)
    setError("")

    register(emailRef.current.value, passwordRef.current.value)
      .then((response) => {
        createMemberAsync({
          firebaseUid: response.user.uid,
          firstName: firstNameRef.current.value,
          lastName: lastNameRef.current.value,
          doB: doBRef.current.value,
        }).then(async () => {
          await logout()
          navigate("/login")
        })
      })
      .catch((err) => {
        setError("Neuspješna registracija")
        console.log(err)
      })
      .finally(() => {
        setLoading(false)
      })
  }

  return (
    <FormContainer formTitle="Registracija" error={error}>
      <Form onSubmit={handleSubmit}>
        <Form.Group id="fname">
          <Form.Label>Ime</Form.Label>
          <Form.Control type="text" ref={firstNameRef} required />
        </Form.Group>
        <Form.Group id="lname">
          <Form.Label>Prezime</Form.Label>
          <Form.Control type="text" ref={lastNameRef} required />
        </Form.Group>
        <Form.Group id="dob">
          <Form.Label>Datum rođenja</Form.Label>
          <Form.Control type="date" ref={doBRef} required />
        </Form.Group>
        <Form.Group id="email">
          <Form.Label>Email</Form.Label>
          <Form.Control type="email" ref={emailRef} required />
        </Form.Group>
        <Form.Group id="password">
          <Form.Label>Lozinka</Form.Label>
          <Form.Control type="password" ref={passwordRef} required />
        </Form.Group>
        <Form.Group id="password-confirm">
          <Form.Label>Potvrda lozinke</Form.Label>
          <Form.Control type="password" ref={passwordConfirmRef} required />
        </Form.Group>
        <Button
          disabled={loading}
          className="w-100 mt-3 btn-success shadow-none"
          type="submit">
          Registriraj se
        </Button>
      </Form>
      <div className="w-100 text-center mt-3">
        Već imaš profil?{" "}
        <Link className="btn btn-success p-2" to="/login">
          Prijavi se
        </Link>
      </div>
    </FormContainer>
  )
}
