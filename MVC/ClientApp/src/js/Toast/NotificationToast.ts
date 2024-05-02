import {Toast} from "bootstrap";

const toast = new Toast(document.getElementById("NotificationToast")!);
let closeNotificationToast = document.getElementById("closeNotificationToast") as HTMLButtonElement

export function showNotificationToast(message: string) {
    const toastBody = document.getElementById("notificationBody") as HTMLDivElement
    toastBody.textContent = message
    toast.show();
}

export function hideNotificationToast() {
    toast.hide();
}

closeNotificationToast.onclick = () => hideNotificationToast()