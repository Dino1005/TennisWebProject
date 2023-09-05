import React, { useContext, useState, useEffect } from "react"
import { auth } from "../services/FirebaseService.js"

const AppContext = React.createContext()

export function useAuth() {
  return useContext(AppContext)
}

export function AuthProvider({ children }) {
  const [currentUser, setCurrentUser] = useState()
  const [loading, setLoading] = useState(true)
  const [userId, setUserId] = useState("")
  const [date, setDate] = useState(new Date().toISOString().slice(0, 10))

  function register(email, password) {
    return auth.createUserWithEmailAndPassword(email, password)
  }

  function login(email, password) {
    return auth.signInWithEmailAndPassword(email, password)
  }

  function logout() {
    return auth.signOut()
  }

  function updateEmail(email) {
    return currentUser.updateEmail(email)
  }

  function updatePassword(password) {
    return currentUser.updatePassword(password)
  }

  useEffect(() => {
    const unsubscribe = auth.onAuthStateChanged((user) => {
      setCurrentUser(user)
      setLoading(false)
    })

    return unsubscribe
  }, [])

  useEffect(() => {
    if (currentUser) {
      currentUser
        .getIdTokenResult()
        .then((idTokenResult) => {
          setUserId(idTokenResult.claims.memberId)
        })
        .catch((error) => {
          console.log(error)
        })
    }
  }, [currentUser])

  const value = {
    currentUser,
    userId,
    date,
    setDate,
    login,
    register,
    logout,
    updateEmail,
    updatePassword,
  }

  return (
    <AppContext.Provider value={value}>
      {!loading && children}
    </AppContext.Provider>
  )
}
