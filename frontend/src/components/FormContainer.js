import React from "react"
import { Card, Alert } from "react-bootstrap"
import { Link } from "react-router-dom"

export default function FormContainer({ formTitle, error, children }) {
  return (
    <Card
      className="d-flex align-items-center justify-content-center login-container"
      style={{
        minHeight: "100vh",
        backgroundColor: "var(--secondary-light)",
      }}>
      <div className="w-100 card-container d-flex align-items-center justify-content-center">
        <Card.Body style={{ maxWidth: "400px" }}>
          <h2 className="text-center mb-4">{formTitle}</h2>
          {error && <Alert variant="danger">{error}</Alert>}
          {children}
        </Card.Body>
      </div>
      <Link to="/" className="btn btn-success mt-5 p-3">
        Natrag
      </Link>
    </Card>
  )
}
