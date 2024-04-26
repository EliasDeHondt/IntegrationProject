import {Modal} from "bootstrap";
import {deleteUser} from "./API/DeleteUserAPI";
import {resetCards} from "./API/DashboardAPI";

let deleteButtons: NodeListOf<HTMLButtonElement>;
let email: string;

const deleteUserModal = new Modal(document.getElementById("deleteUserModal")!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
})
const butConfirmDeleteUser = document.getElementById("butConfirmDeleteUser") as HTMLButtonElement
const butCancelDeleteUserModal = document.getElementById("butCancelDeleteUserModal") as HTMLButtonElement
const userRoulette = document.getElementById("carouselContainer") as HTMLDivElement;

let id: string = document.getElementById("platformId")!.textContent!
export function initializeDeleteButtons() {
    deleteButtons = document.querySelectorAll(".deleteUser");
    deleteButtons.forEach((button) => {
        button.onclick = () => {
            let card = button.parentNode!.parentNode as HTMLDivElement;
            email = card.children[1].children[1].textContent!
            email = email.substring(7)
            deleteUserModal.show();
        }
    })
}

butConfirmDeleteUser.onclick = () => {
    deleteUser(email).then(() => {
        deleteUserModal.hide();
        resetCards(id, userRoulette, true);
    });
}

butCancelDeleteUserModal.onclick = () => {
    deleteUserModal.hide();
}

