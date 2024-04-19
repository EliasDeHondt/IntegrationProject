import {Modal} from "bootstrap";
import { Project } from "../ProjectObjects";

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

let id = document.getElementById("platformId")!.textContent

butCreateUser.onclick = () => {
    CreateUserModal.show();
}

butCloseCreateUserModal.onclick = () => {
    clearModal()
}

butCancelCreateUserModal.onclick = () => {
    clearModal()
}

butConfirmCreateUser.onclick = (ev) => {
    ev.preventDefault()
    // API call to create user
    if(radioFacilitator.checked) createFacilitator()
    if(radioAdmin.checked) createAdmin()
}

radioAdmin.onchange = () => {
    facilitatorContainer.classList.add("visually-hidden");
    selectProject.options.length = 0;
}

radioFacilitator.onchange = () => {

    facilitatorContainer.classList.remove("visually-hidden");
    // Generate options for each Project
    getProjects();
    
}

function clearModal(){
    inputName.value = "";
    inputEmail.value = "";
    inputPassword.value = "";
    CreateUserModal.hide();
}

function createFacilitator(){
    let projectIds: number[] = [];
    for (let i = 0; i < selectProject.options.length; i++) {
        if (selectProject.options[i].selected) {
            projectIds.push(Number(selectProject.options[i].value));
        }
    }
    fetch("/api/Users/CreateFacilitator", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: inputName.value,
            email: inputEmail.value,
            password: inputPassword.value,
            projectIds: projectIds,
            platformId: id
        })
    })
        .then(() => clearModal())
}
function createAdmin(){
    fetch("/api/Users/CreateAdmin", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: inputName.value,
            email: inputEmail.value,
            password: inputPassword.value,
            platformId: id
        })
    })
        .then(() => clearModal())
}

function getProjects() {
    fetch(`/api/Projects/GetProjectsForSharedPlatform/${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => fillDropdown(data))
}

function fillDropdown(data: Project[]) {
    for (let i = 0; i < data.length; i++) {
        let option = document.createElement("option");
        option.value = data[i].id.toString();
        option.text = data[i].mainTheme.subject;
        selectProject.appendChild(option);
    }
}