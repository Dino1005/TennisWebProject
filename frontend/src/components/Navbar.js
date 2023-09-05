import "../App.css"
import { Button } from "react-bootstrap"
import { useAuth } from "../contexts/AppContext"
import { Link, useNavigate } from "react-router-dom"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faUser } from "@fortawesome/free-solid-svg-icons"
import { confirmAlert } from "react-confirm-alert"
import "react-confirm-alert/src/react-confirm-alert.css"

export default function Navbar({}) {
  const { logout } = useAuth()
  const navigate = useNavigate()

  async function handleLogout() {
    try {
      confirmAlert({
        title: "Jeste li sigurni da se želite odjaviti?",
        buttons: [
          {
            label: "Da",
            onClick: async () => await logout(),
          },
          {
            label: "Ne",
          },
        ],
      })
    } catch (err) {
      console.log(err)
    }
  }

  return (
    <div className="nav-container">
      <nav>
        <div className="nav-content">
          <div className="nav-title">
            <img
              src={require("../assets/logo.png")}
              alt="Logo"
              onClick={() => navigate("/")}></img>
            <span className="nav-title-text">Teniski Klub Donji Miholjac</span>
          </div>
          <div className="nav-btn">
            <Link to="/profile">
              <FontAwesomeIcon
                icon={faUser}
                style={{ width: "30px", height: "30px" }}
              />
            </Link>
            <div className="w-100 text-center">
              <Button className="btn btn-success w-100" onClick={handleLogout}>
                Odjava
              </Button>
            </div>
          </div>
        </div>
      </nav>
    </div>
  )
}
