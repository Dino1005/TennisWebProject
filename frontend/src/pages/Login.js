import React, { useRef, useState } from "react"
import { Form, Button } from "react-bootstrap"
import { useAuth } from "../contexts/AppContext"
import { Link, useNavigate } from "react-router-dom"
import FormContainer from "../components/FormContainer"

export default function Login() {
  const emailRef = useRef()
  const passwordRef = useRef()
  const { login } = useAuth()
  const [error, setError] = useState("")
  const [loading, setLoading] = useState(false)
  const navigate = useNavigate()

  async function handleSubmit(e) {
    e.preventDefault()

    try {
      setError("")
      setLoading(true)
      await login(emailRef.current.value, passwordRef.current.value)
      navigate("/")
    } catch {
      setError("Prijava neuspješna")
    }

    setLoading(false)
  }

  return (
    <FormContainer formTitle="Prijava" error={error}>
      <Form onSubmit={handleSubmit}>
        <Form.Group id="email">
          <Form.Label>Email</Form.Label>
          <Form.Control type="email" ref={emailRef} required />
        </Form.Group>
        <Form.Group id="password">
          <Form.Label>Lozinka</Form.Label>
          <Form.Control type="password" ref={passwordRef} required />
        </Form.Group>
        <Button
          disabled={loading}
          className="w-100 mt-3 btn-success shadow-none"
          type="submit">
          Prijavi se
        </Button>
      </Form>
      <div className="w-100 text-center mt-4">
        Nemaš profil?{" "}
        <Link className="btn btn-success p-2" to="/register">
          Registriraj se
        </Link>
      </div>
    </FormContainer>
  )
}
