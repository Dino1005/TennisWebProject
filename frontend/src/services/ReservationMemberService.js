import { getHeaders } from "./ApiService"
import API from "./ApiService"

export async function getReservationMembersAsync(pageLength, pageNumber) {
  try {
    const response = await API.get(
      `/reservationMember?pageSize=${pageLength}&pageNumber=${pageNumber}`,
      { headers: getHeaders() }
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getReservationMembersByReservationIdAsync(
  pageLength,
  pageNumber,
  reservationId
) {
  try {
    const response = await API.get(
      `/reservationMember?pageSize=${pageLength}&pageNumber=${pageNumber}&reservationId=${reservationId}`,
      { headers: getHeaders() }
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getReservationMembersByMemberIdAsync(
  pageLength,
  pageNumber,
  memberId
) {
  try {
    const response = await API.get(
      `/reservationMember?pageSize=${pageLength}&pageNumber=${pageNumber}&memberId=${memberId}`,
      { headers: getHeaders() }
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getReservationMemberByIdAsync(id) {
  try {
    const response = await API.get(`/reservationMember/${id}`, {
      headers: getHeaders(),
    })
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function createReservationMemberAsync(reservationMember) {
  try {
    await API.post("/reservationMember", reservationMember, {
      headers: getHeaders(),
    })
  } catch (error) {
    console.log(error)
  }
}

export async function updateReservationMemberAsync(id, reservationMember) {
  try {
    await API.put(`/reservationMember/${id}`, reservationMember, {
      headers: getHeaders(),
    })
  } catch (error) {
    console.log(error)
  }
}

export async function toggleReservationMemberAsync(id) {
  try {
    await API.delete(`/reservationMember/toggle/${id}`, {
      headers: getHeaders(),
    })
  } catch (error) {
    console.log(error)
  }
}
