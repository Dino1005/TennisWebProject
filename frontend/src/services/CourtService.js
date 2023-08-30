import { getHeaders } from "./ApiService"
import API from "./ApiService"

export async function getCourtsAsync(pageLength, pageNumber) {
  try {
    const response = await API.get(
      `/court?pageSize=${pageLength}&pageNumber=${pageNumber}`,
      { headers: getHeaders() }
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getCourtsByNameAsync(pageLength, pageNumber, name) {
  try {
    const response = await API.get(
      `/court?pageSize=${pageLength}&pageNumber=${pageNumber}&name=${name}`,
      { headers: getHeaders() }
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getCourtByIdAsync(id) {
  try {
    const response = await API.get(`/court/${id}`, { headers: getHeaders() })
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function createCourtAsync(court) {
  try {
    await API.post("/court", court, { headers: getHeaders() })
  } catch (error) {
    console.log(error)
  }
}

export async function updateCourtAsync(id, court, navigate) {
  try {
    await API.put(`/court/${id}`, court, { headers: getHeaders() })
  } catch (error) {
    console.log(error)
  }
}

export async function toggleCourtAsync(id) {
  try {
    await API.delete(`/court/toggle/${id}`, { headers: getHeaders() })
  } catch (error) {
    console.log(error)
  }
}
