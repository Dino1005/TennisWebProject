import API from "./ApiService"

export async function getCourtsAsync(pageLength, pageNumber) {
  try {
    const response = await API.get(
      `/court?pageSize=${pageLength}&pageNumber=${pageNumber}&orderBy=\"Name\"`
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getCourtsByNameAsync(pageLength, pageNumber, name) {
  try {
    const response = await API.get(
      `/court?pageSize=${pageLength}&pageNumber=${pageNumber}&name=${name}`
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getCourtByIdAsync(id) {
  try {
    const response = await API.get(`/court/${id}`)
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function createCourtAsync(court) {
  try {
    await API.post("/court", court)
  } catch (error) {
    console.log(error)
  }
}

export async function updateCourtAsync(id, court, navigate) {
  try {
    await API.put(`/court/${id}`, court)
  } catch (error) {
    console.log(error)
  }
}

export async function toggleCourtAsync(id) {
  try {
    await API.delete(`/court/toggle/${id}`)
  } catch (error) {
    console.log(error)
  }
}
