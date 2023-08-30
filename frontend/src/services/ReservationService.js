import { getHeaders } from "./ApiService"
import API from "./ApiService"

export async function getReservationsAsync(pageLength, pageNumber) {
  try {
    const response = await API.get(
      `/reservation?pageSize=${pageLength}&pageNumber=${pageNumber}`,
      { headers: getHeaders() }
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getReservationsByTimeAsync(pageLength, pageNumber, time) {
  try {
    const response = await API.get(
      `/reservation?pageSize=${pageLength}&pageNumber=${pageNumber}&time=${time}`,
      { headers: getHeaders() }
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getReservationsByCourtIdAsync(
  pageLength,
  pageNumber,
  courtId
) {
  try {
    const response = await API.get(
      `/reservation?pageSize=${pageLength}&pageNumber=${pageNumber}&courtId=${courtId}`,
      { headers: getHeaders() }
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getReservationByIdAsync(id) {
  try {
    const response = await API.get(`/reservation/${id}`, {
      headers: getHeaders(),
    })
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function createReservationAsync(reservation) {
  try {
    await API.post("/reservation", reservation, { headers: getHeaders() })
  } catch (error) {
    console.log(error)
  }
}

export async function updateReservationAsync(id, reservation) {
  try {
    await API.put(`/reservation/${id}`, reservation, { headers: getHeaders() })
  } catch (error) {
    console.log(error)
  }
}

export async function toggleReservationAsync(id) {
  try {
    await API.delete(`/reservation/toggle/${id}`, { headers: getHeaders() })
  } catch (error) {
    console.log(error)
  }
}
