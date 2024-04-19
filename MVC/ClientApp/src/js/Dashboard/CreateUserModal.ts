import {Dropdown, Modal} from "bootstrap";
import {Select} from "@tensorflow/tfjs";

const CreateUserModal = new Modal(document.getElementById('CreateUserModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

const butCreateUser = document.getElementById("butCreateUser") as HTMLButtonElement;
const butCloseCreateUserModal = document.getElementById("butCloseCreateUserModal") as HTMLButtonElement;
const butCancelCreateUserModal = document.getElementById("butCancelCreateUserModal") as HTMLButtonElement;
const butConfirmCreateUser = document.getElementById("butConfirmCreateUser") as HTMLButtonElement;

const radioAdmin = document.getElementById("radioAdmin") as HTMLInputElement;
const radioFacilitator = document.getElementById("radioFacilitator") as HTMLInputElement;
const facilitatorContainer = document.getElementById("facilitatorContainer") as HTMLDivElement;

const inputName = document.getElementById("inputName") as HTMLInputElement;
const inputEmail = document.getElementById("inputEmail") as HTMLInputElement;
const inputPassword = document.getElementById("inputPassword") as HTMLInputElement;
const selectProject = document.getElementById("selectProject") as HTMLSelectElement;

butCreateUser.onclick = () => {
    CreateUserModal.show();
}

butCloseCreateUserModal.onclick = () => {
    CreateUserModal.hide();
}

butCancelCreateUserModal.onclick = () => {
    inputName.value = "";
    inputEmail.value = "";
    inputPassword.value = "";
    CreateUserModal.hide();
}

butConfirmCreateUser.onclick = (ev) => {
    ev.preventDefault()
    // API call to create user
    let projectIds: number[] = [];
    for (const option in selectProject.selectedOptions) {
        projectIds.push(Number(selectProject.selectedOptions[option].value));
    }
    
    fetch("api/Users/CreateFacilitator", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: inputName.value,
            email: inputEmail.value,
            password: inputPassword.value,
            projectids: projectIds
        })
    })
        .then(() => CreateUserModal.hide())
}

radioAdmin.onclick = () => {
    facilitatorContainer.classList.add("visually-hidden");
}

radioFacilitator.onclick = () => {

    facilitatorContainer.classList.remove("visually-hidden");
    
    // Generate options for each Project
}

