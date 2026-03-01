(function () {

    function isIOS() {
        return /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;
    }

    if (isIOS()) {
        window.Fcm = {
            requestAndGetToken: async () => null
        };
        return;
    }
    if (!firebase.apps.length) {
        firebase.initializeApp({
            apiKey: "AIzaSyCdRN4Xo17Pomocu6pad0NuzUMKs1W5c3I",
            authDomain: "alfkhani-e0981.firebaseapp.com",
            projectId: "alfkhani-e0981",
            messagingSenderId: "585119617535",
            appId: "1:585119617535:web:462d3523d26c3756a2d7ca"
        });
    }

    const messaging = firebase.messaging();

    async function requestAndGetToken() {
        try {
            const registration = await navigator.serviceWorker.ready;

            const permission = await Notification.requestPermission();
            if (permission !== 'granted') {
                throw new Error('Notification permission not granted');
            }

            const token = await messaging.getToken({
                vapidKey: "BNUNoe1TpmxNYcfpNk4qaYSI5ipIdDqsR_t75K-quxD_QX109GsKmQD0mXnIefykYJ_g5RFyILBW9pS0XePlKzY",
                serviceWorkerRegistration: registration
            });

            if (!token) {
                throw new Error('FCM token is null');
            }

            return token;
        } catch (e) {
            console.error("FCM getToken error:", e);
            throw e;
        }
    }

    messaging.onMessage(payload => {
        console.log("FCM foreground message:", payload);
    });

    window.Fcm = {
        requestAndGetToken
    };

})();
