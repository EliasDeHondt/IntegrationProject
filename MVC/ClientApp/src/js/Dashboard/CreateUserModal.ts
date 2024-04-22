import {Modal, Toast} from "bootstrap";
import * as API from "./API/CreateUserModalAPI";
import {Project} from "./Types/ProjectObjects";
import {resetCards} from "./API/DashboardAPI";

const CreateUserModal = new Modal(document.getElementById('CreateUserModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

const userCreatedToast = new Toast(document.getElementById("userToast")!);

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

const nameWarning = document.getElementById('nameWarning') as HTMLElement;
const emailWarning = document.getElementById('emailWarning') as HTMLElement;
const passwordWarning = document.getElementById('passwordWarning') as HTMLElement;

const userRoulette = document.getElementById("UserRoulette") as HTMLDivElement;

let id: string = document.getElementById("platformId")!.textContent!

butCreateUser.onclick = () => {
    CreateUserModal.show();
}

butCloseCreateUserModal.onclick = () => {
    clearModal()
}

butCancelCreateUserModal.onclick = () => {
    clearModal()
}

butConfirmCreateUser.onclick = async (ev) => {
    ev.preventDefault()
    if (await validateForm()) {
        if (radioFacilitator.checked) {
            let projectIds = getSelectedProjects();
            API.createFacilitator(inputName.value, inputEmail.value, inputPassword.value, projectIds, id)
                .then(() => clearModal())
                .then(() => {
                    userCreatedToast.show()
                    let closeUserToast = document.getElementById("closeUserToast") as HTMLButtonElement
                    closeUserToast.onclick = () => userCreatedToast.hide()
                })
                .finally(() => resetCards(id, userRoulette))
        } else if (radioAdmin.checked) {
            API.createAdmin(inputName.value, inputEmail.value, inputPassword.value, id)
                .then(() => clearModal())
                .then(() => {
                    userCreatedToast.show()
                    let closeUserToast = document.getElementById("closeUserToast") as HTMLButtonElement
                    closeUserToast.onclick = () => userCreatedToast.hide()
                })
                .finally(() => resetCards(id, userRoulette))
        }
    }
}

radioAdmin.onchange = () => {
    facilitatorContainer.classList.add("visually-hidden");
    selectProject.options.length = 0;
}

radioFacilitator.onchange = () => {

    facilitatorContainer.classList.remove("visually-hidden");
    // Generate options for each Project
    API.getProjects(id).then(projects => {
        fillDropdown(projects)
    });

}

function clearModal() {
    inputName.value = "";
    inputEmail.value = "";
    inputPassword.value = "";
    radioAdmin.checked = true;
    radioFacilitator.checked = false;
    resetWarnings();
    CreateUserModal.hide();
}

function resetWarnings() {
    nameWarning.textContent = '';
    emailWarning.textContent = '';
    passwordWarning.textContent = '';
}

async function validateForm() {
    let valid: boolean = true;


    if (inputName.value.trim() === '') {
        nameWarning.textContent = 'Please enter a name';
        valid = false;
    } else {
        nameWarning.textContent = '';
    }

    let emailRegex: RegExp = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (inputEmail.value.trim() === '') {
        emailWarning.textContent = 'Please enter an email';
        valid = false;
    } else if (!emailRegex.test(inputEmail.value)) {
        emailWarning.textContent = 'Please enter a valid email';
        valid = false;
    } else if (await API.isEmailInUse(inputEmail.value)) {
        emailWarning.textContent = 'This email is already in use';
        valid = false;
    } else {
        emailWarning.textContent = '';
    }

    let passwordRegex: RegExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{10,}$/

    if (inputPassword.value.trim() === '') {
        passwordWarning.textContent = 'Please enter a password';
        valid = false;
    } else if (!passwordRegex.test(inputPassword.value)) {
        passwordWarning.textContent = 'The password needs to be 10 characters long and contain at least one uppercase letter, one lowercase letter, one number and one special character';
        valid = false;
    } else {
        passwordWarning.textContent = '';
    }

    return valid;
}

function getSelectedProjects(): number[] {
    let projectIds: number[] = [];
    for (let i = 0; i < selectProject.options.length; i++) {
        if (selectProject.options[i].selected) {
            projectIds.push(Number(selectProject.options[i].value));
        }
    }
    return projectIds
}

function fillDropdown(data: Project[]) {
    for (let i = 0; i < data.length; i++) {
        let option = document.createElement("option");
        option.value = data[i].id.toString();
        option.text = data[i].mainTheme.subject;
        selectProject.appendChild(option);
    }
}