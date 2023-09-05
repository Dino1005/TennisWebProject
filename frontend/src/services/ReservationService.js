import API from "./ApiService"

export async function getReservationsAsync(pageLength, pageNumber) {
  try {
    const response = await API.get(
      `/reservation?pageSize=${pageLength}&pageNumber=${pageNumber}`
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getReservationsByTimeAndCourtIdAsync(
  pageLength,
  pageNumber,
  time,
  courtId
) {
  try {
    const response = await API.get(
      `/reservation?pageSize=${pageLength}&pageNumber=${pageNumber}&time=${time}&courtId=${courtId}`
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
      `/reservation?pageSize=${pageLength}&pageNumber=${pageNumber}&courtId=${courtId}`
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getReservationsByMemberIdAsync(
  pageLength,
  pageNumber,
  memberId
) {
  try {
    const response = await API.get(
      `/reservation?pageSize=${pageLength}&pageNumber=${pageNumber}&memberId=${memberId}&orderBy=\"Time\"`
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getReservationByIdAsync(id) {
  try {
    const response = await API.get(`/reservation/${id}`)
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function createReservationAsync(reservation) {
  try {
    await API.post("/reservation", reservation)
  } catch (error) {
    console.log(error)
  }
}

export async function updateReservationAsync(id, reservation) {
  try {
    await API.put(`/reservation/${id}`, reservation)
  } catch (error) {
    console.log(error)
  }
}

export async function toggleReservationAsync(id) {
  try {
    await API.delete(`/reservation/toggle/${id}`)
  } catch (error) {
    console.log(error)
  }
}
