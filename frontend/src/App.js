import "./App.css"
import { Navigate, Route, Routes } from "react-router-dom"
import PrivateRoute from "./components/PrivateRoute"
import Login from "./pages/Login"
import Register from "./pages/Register"
import Home from "./pages/Home"
import Profile from "./pages/Profile"
import Reservation from "./pages/Reservation"
import Member from "./pages/Member"
import About from "./pages/About"
import Court from "./pages/Court"

export default function App() {
  return (
    <>
      <Routes>
        <Route
          exact
          path="/"
          element={
            <PrivateRoute>
              <Home />
            </PrivateRoute>
          }></Route>
        <Route
          exact
          path="/profile"
          element={
            <PrivateRoute>
              <Profile />
            </PrivateRoute>
          }></Route>
        <Route
          path="/reservation"
          element={
            <PrivateRoute>
              <Reservation />
            </PrivateRoute>
          }></Route>
        <Route
          path="/court/:id"
          element={
            <PrivateRoute>
              <Court />
            </PrivateRoute>
          }></Route>
        <Route
          path="/member"
          element={
            <PrivateRoute>
              <Member />
            </PrivateRoute>
          }></Route>
        <Route
          path="/about"
          element={
            <PrivateRoute>
              <About />
            </PrivateRoute>
          }></Route>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="*" element={<Navigate to={"/"} />} />
      </Routes>
    </>
  )
}
