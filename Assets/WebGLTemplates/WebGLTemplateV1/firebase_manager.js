import { initializeApp } from 'https://www.gstatic.com/firebasejs/12.9.0/firebase-app.js'

// Add Firebase products that you want to use
import { getAuth } from 'https://www.gstatic.com/firebasejs/12.9.0/firebase-auth.js'
import { getFirestore, collection, addDoc } from 'https://www.gstatic.com/firebasejs/12.9.0/firebase-firestore.js'

// Your web app's Firebase configuration
const firebaseConfig = {
    apiKey: "AIzaSyC9bIdRTTpjCvfoqidbHvaBo5YyUbrwdiU",
    authDomain: "pencils-down-player-analytics.firebaseapp.com",
    projectId: "pencils-down-player-analytics",
    storageBucket: "pencils-down-player-analytics.firebasestorage.app",
    messagingSenderId: "414442451480",
    appId: "1:414442451480:web:5c8b7bc703723b2775896d"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);

// get Cloud Firestore instance
const db = getFirestore(app);

try {
    const docRef = await addDoc(collection(db, "test"), {
        testField: "testValue2",
        testField2: 246,
    });
    console.log("Document written with ID: ", docRef.id);
} catch (e) {
    console.error("Error adding document: ", e);
}