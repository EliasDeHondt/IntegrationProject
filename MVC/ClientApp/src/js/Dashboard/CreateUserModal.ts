import {Modal} from "bootstrap";

const CreateUserModal = new Modal(document.getElementById('CreateUserModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

const butCreateUser = document.getElementById("butCreateUser") as HTMLButtonElement;

butCreateUser.onclick = () => {
    CreateUserModal.show();
}