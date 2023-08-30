import firebase from "firebase/compat/app"
import "firebase/compat/auth"

const app = firebase.initializeApp({
  apiKey: "AIzaSyDOeH6LKWOaVnPaYInaSA7aw5Vi0Qey0Os",
  authDomain: "tennisreservations-e9e14.firebaseapp.com",
  projectId: "tennisreservations-e9e14",
  storageBucket: "tennisreservations-e9e14.appspot.com",
  messagingSenderId: "397854488302",
  appId: "1:397854488302:web:b8a3fd39451ea7cf56e5ff",
  measurementId: "G-EHZ99RW4N6",
})

export const auth = app.auth()
export default app
