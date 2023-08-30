import { useState, createContext, useContext } from "react"

const AppContext = createContext()

export function useAppContext() {
  return useContext(AppContext)
}

export function AppContextProvider({ children }) {
  const [authenticatedUser, setAuthenticatedUser] = useState(null)
  const [selectedDate, setSelectedDate] = useState(() => {
    const curr = new Date()
    return curr.toISOString().substring(0, 10)
  })

  return (
    <ResultContext.Provider
      value={{
        authenticatedUser,
        setAuthenticatedUser,
        selectedDate,
        setSelectedDate,
      }}>
      {children}
    </ResultContext.Provider>
  )
}
