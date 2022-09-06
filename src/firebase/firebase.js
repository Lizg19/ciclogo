import { initializeApp } from "firebase/app";
import { getAuth } from "firebase/auth";
import 'firebase/database'
import { getDatabase } from "firebase/database";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries
const firebaseConfig = {
  apiKey: "AIzaSyBf7Y3G6mfUvZ31X1W1SvnKjhHhX8ugYkk",
  authDomain: "ciclismoapp-60fa5.firebaseapp.com",
  databaseURL: "https://ciclismoapp-60fa5-default-rtdb.firebaseio.com",
  projectId: "ciclismoapp-60fa5",
  storageBucket: "ciclismoapp-60fa5.appspot.com",
  messagingSenderId: "272471059192",
  appId: "1:272471059192:web:fa9df458e524a54ca4690d",
  measurementId: "G-317N1Z4M8N"
};

// Initialize Firebase
export const app = initializeApp(firebaseConfig);
export const auth = getAuth();
export const db = getDatabase(app);


