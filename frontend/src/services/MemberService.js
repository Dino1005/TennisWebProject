import API from "./ApiService"

export async function getMembersAsync(pageLength, pageNumber) {
  try {
    const response = await API.get(
      `/member?pageSize=${pageLength}&pageNumber=${pageNumber}`
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getMembersByNameAsync(pageLength, pageNumber, name) {
  try {
    const response = await API.get(
      `/member?pageSize=${pageLength}&pageNumber=${pageNumber}&name=${name}`
    )
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function getMemberByIdAsync(id) {
  try {
    const response = await API.get(`/member/${id}`)
    return response.data
  } catch (error) {
    console.log(error)
  }
}

export async function createMemberAsync(member) {
  try {
    await API.post("/member", member)
  } catch (error) {
    console.log(error)
  }
}

export async function updateMemberAsync(id, member) {
  try {
    await API.put(`/member/${id}`, member)
  } catch (error) {
    console.log(error)
  }
}

export async function toggleMemberAsync(id) {
  try {
    await API.delete(`/member/toggle/${id}`)
  } catch (error) {
    console.log(error)
  }
}
