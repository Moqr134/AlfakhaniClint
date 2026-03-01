importScripts('https://www.gstatic.com/firebasejs/10.7.1/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/10.7.1/firebase-messaging-compat.js');

firebase.initializeApp({
    apiKey: "AIzaSyCdRN4Xo17Pomocu6pad0NuzUMKs1W5c3I",
    authDomain: "alfkhani-e0981.firebaseapp.com",
    projectId: "alfkhani-e0981",
    messagingSenderId: "585119617535",
    appId: "1:585119617535:web:462d3523d26c3756a2d7ca"
});

const messaging = firebase.messaging();

messaging.onBackgroundMessage(payload => {
    self.registration.showNotification(
        payload.notification?.title ?? 'Notification',
        {
            body: payload.notification?.body ?? '',
            icon: '/images/Fakhani.png'
        }
    );
});