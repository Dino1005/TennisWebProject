import axios from "axios"

export default axios.create({
  baseURL: "https://localhost:44345/api",
})

export function getHeaders() {
  return {
    Authorization: `Bearer ${localStorage.getItem("token")}`,
  }
}
