import { getHeaders } from "./ApiService"
import API from "./ApiService"

export async function getMembersAsync(pageLength, pageNumber) {
  try {
    const response = await API.get(
      `/member?pageSize=${pageLength}&pageNumber=${pageNumber}`,
      { headers: getHeaders() }
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getMembersByNameAsync(pageLength, pageNumber, name) {
  try {
    const response = await API.get(
      `/member?pageSize=${pageLength}&pageNumber=${pageNumber}&name=${name}`,
      { headers: getHeaders() }
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getMemberByIdAsync(id) {
  try {
    const response = await API.get(`/member/${id}`, { headers: getHeaders() })
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function createMemberAsync(member) {
  try {
    await API.post("/member", member, { headers: getHeaders() })
  } catch (error) {
    console.log(error)
  }
}

export async function updateMemberAsync(id, member) {
  try {
    await API.put(`/member/${id}`, member, { headers: getHeaders() })
  } catch (error) {
    console.log(error)
  }
}

export async function toggleMemberAsync(id) {
  try {
    await API.delete(`/member/toggle/${id}`, { headers: getHeaders() })
  } catch (error) {
    console.log(error)
  }
}
