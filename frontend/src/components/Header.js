import React from "react"

export default function Header({ title, home }) {
  return (
    <div className="header-container d-flex align-items-center justify-content-center">
      <div
        className={`header-content d-flex align-items-center justify-content-center ${
          home ? "header-content-home" : ""
        }`}>
        <h1 className="header-text">{title}</h1>
      </div>
    </div>
  )
}
