import {Modal} from "bootstrap";

const CreateUserModal = new Modal(document.getElementById('CreateUserModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

const butCreateUser = document.getElementById("butCreateUser") as HTMLButtonElement;
const butCloseCreateUserModal = document.getElementById("butCloseCreateUserModal") as HTMLButtonElement;
const butCancelCreateUserModal = document.getElementById("butCancelCreateUserModal") as HTMLButtonElement;
const butConfirmCreateUser = document.getElementById("butConfirmCreateUser") as HTMLButtonElement;

butCreateUser.onclick = () => {
    CreateUserModal.show();
}

butCloseCreateUserModal.onclick = () => {
    CreateUserModal.hide();
}

butCancelCreateUserModal.onclick = () => {
    // Empty fields
    
    CreateUserModal.hide();
}

butConfirmCreateUser.onclick = () => {
    // API call to create user
    
    CreateUserModal.hide();
}