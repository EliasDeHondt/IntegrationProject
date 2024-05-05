import {Modal, Toast} from "bootstrap";
import * as API from "./API/CreateUserModalAPI";
import {Project} from "../Types/ProjectObjects";
import {resetCards} from "./API/DashboardAPI";
import {UserRoles} from "../Types/UserTypes";
import {isEmailInUse} from "../API/UserAPI";
import {sendEmail} from "./API/CreateUserModalAPI";

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
const adminContainer = document.getElementById("adminContainer") as HTMLDivElement;
const checkUserPermission = document.getElementById("checkUserPermission") as HTMLInputElement;
const checkProjectPermission = document.getElementById("checkProjectPermission") as HTMLInputElement;
const checkStatisticPermission = document.getElementById("checkStatisticPermission") as HTMLInputElement;


const inputName = document.getElementById("inputName") as HTMLInputElement;
const inputEmail = document.getElementById("inputEmail") as HTMLInputElement;
const inputPassword = document.getElementById("inputPassword") as HTMLInputElement;
const selectProject = document.getElementById("selectProject") as HTMLSelectElement;

const nameWarning = document.getElementById('nameWarning') as HTMLElement;
const emailWarning = document.getElementById('emailWarning') as HTMLElement;
const passwordWarning = document.getElementById('passwordWarning') as HTMLElement;

const userRoulette = document.getElementById("carouselContainer") as HTMLDivElement;

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
                .then(() => {
                    sendEmail(inputEmail.value, inputPassword.value)
                    clearModal()
                })
                .then(() => {
                    userCreatedToast.show()
                    let closeUserToast = document.getElementById("closeUserToast") as HTMLButtonElement
                    closeUserToast.onclick = () => userCreatedToast.hide()
                })
                .finally(() => {
                    resetCards(id, userRoulette, true)

                })
        } else if (radioAdmin.checked) {
            let permissions: string[] = [];
            if (checkUserPermission.checked) {
                permissions.push(UserRoles.UserPermission);
            }
            if (checkProjectPermission.checked) {
                permissions.push(UserRoles.ProjectPermission);
            }
            if (checkStatisticPermission.checked) {
                permissions.push(UserRoles.StatisticPermission);
            }
            API.createAdmin(inputName.value, inputEmail.value, inputPassword.value, id, permissions)
                .then(() => {
                    sendEmail(inputEmail.value, inputPassword.value)
                    clearModal()
                })
                .then(() => {
                    userCreatedToast.show()
                    let closeUserToast = document.getElementById("closeUserToast") as HTMLButtonElement
                    closeUserToast.onclick = () => userCreatedToast.hide()
                })
                .finally(() => {
                    resetCards(id, userRoulette, true)
                })
        }
    }
}

radioAdmin.onchange = () => {
    facilitatorContainer.classList.add("visually-hidden");
    adminContainer.classList.remove("visually-hidden");
    selectProject.options.length = 0;
}

radioFacilitator.onchange = () => {

    facilitatorContainer.classList.remove("visually-hidden");
    adminContainer.classList.add("visually-hidden");
    checkUserPermission.checked = false;
    checkProjectPermission.checked = false;
    checkStatisticPermission.checked = false;
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
    checkUserPermission.checked = false;
    checkProjectPermission.checked = false;
    checkStatisticPermission.checked = false;
    facilitatorContainer.classList.add("visually-hidden");
    adminContainer.classList.remove("visually-hidden");
    selectProject.length = 0;
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
    } else if (await isEmailInUse(inputEmail.value)) {
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