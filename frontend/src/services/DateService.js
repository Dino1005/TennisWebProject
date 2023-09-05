export function extractHours(fullDate) {
  const date = new Date(fullDate)
  const hours = date.getHours()

  return `${hours}:00 - ${hours + 1}:00`
}

export function getHour(time) {
  const date = new Date(time)
  return date.getHours()
}

export function extractDate(fullDate) {
  const date = new Date(fullDate)
  const day = date.getDate()
  const month = date.getMonth() + 1
  const year = date.getFullYear()
  return `${day}. ${month}. ${year}.`
}

export function getDayOfWeek(dateString) {
  const daysOfWeek = [
    "Nedjelja",
    "Ponedjeljak",
    "Utorak",
    "Srijeda",
    "Četvrtak",
    "Petak",
    "Subota",
  ]

  const [day, month, year] = dateString.split(".").map(Number)
  const date = new Date(year, month - 1, day)
  const dayOfWeek = daysOfWeek[date.getDay()]
  return dayOfWeek
}
