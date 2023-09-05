import React from "react"
import Navbar from "../components/Navbar"
import Header from "../components/Header"

export default function About() {
  return (
    <div>
      <Navbar />
      <Header title={"Informacije o klubu"} />
      <div className="about-container">
        <div className="about-text">
          <h2>Teniski Klub Donji Miholjac</h2>
          <p>
            Teniski klub Donji Miholjac osnovan je 1997. godine. Klub trenutno
            broji 350 članova, a treninzi se održavaju na teniskim terenima
            Sportsko-rekreacijskog centra Donji Miholjac. Klub je član Teniskog
            saveza Osječko-baranjske županije i Hrvatskog teniskog saveza. U
            klubu djeluje škola tenisa za djecu i odrasle, a organiziraju se i
            turniri za sve uzraste. Uz redovne treninge, klub organizira i
            ljetne teniske kampove za djecu.
          </p>
        </div>
        <div className="about-footer">
          <img src={require("../assets/court.png")} alt="Tenis" />
          <div>
            <h1>Kontakt</h1>
            <ul>
              <li>
                <strong>Adresa:</strong> Augusta Cesarca 6
              </li>
              <li>
                <strong>Grad:</strong> 31540 Donji Miholjac
              </li>
              <li>
                <strong>Država:</strong> Hrvatska
              </li>
              <li>
                <strong>Mobitel:</strong> +385 91 8852 410
              </li>
              <li>
                <strong>Email:</strong> vatroslav.srajer@gmail.com
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  )
}
