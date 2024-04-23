import {Modal} from "bootstrap";
import * as API from "./API/EditUserModalAPI";
import {FacilitatorUpdate, UserRoles} from "./Types/UserTypes";
import {User} from "./Types/UserTypes";
import {resetCards} from "./API/DashboardAPI";
import {isEmailInUse, isUserInRole} from "../API/UserAPI";
import {Project} from "./Types/ProjectObjects";

let editButtons: HTMLCollectionOf<HTMLButtonElement>;
let email: string;
const editUserModal = new Modal(document.getElementById("EditUserModal")!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
})
const nameInput = document.getElementById("inpEditName") as HTMLInputElement
const emailInput = document.getElementById("inpEditEmail") as HTMLInputElement
const butConfirmEditUser = document.getElementById("butConfirmEditUser") as HTMLButtonElement
const butCancelEditUserModal = document.getElementById("butCancelEditUserModal") as HTMLButtonElement
const butCloseEditUserModal = document.getElementById("butCloseEditUserModal") as HTMLButtonElement

const userRoulette = document.getElementById("carouselContainer") as HTMLDivElement;

let id: string = document.getElementById("platformId")!.textContent!

export function initializeEditButtons() {
    editButtons = document.getElementsByClassName("editUser") as HTMLCollectionOf<HTMLButtonElement>;
    for (let i = 0; i < editButtons.length; i++) {
        editButtons[i].onclick = () => {
            let card = editButtons[i].parentNode!.parentNode as HTMLDivElement;
            email = card.children[1].children[1].textContent!
            email = email.substring(7)
            fillModal(email);
        }
    }
}

function resetWarnings() {
    let nameWarning = document.getElementById('nameWarning') as HTMLElement;
    let emailWarning = document.getElementById('emailWarning') as HTMLElement;
    nameWarning.textContent = '';
    emailWarning.textContent = '';
}

async function validateModal(): Promise<boolean>{
    let valid: boolean = true;
    let nameWarning = document.getElementById('editNameWarning') as HTMLElement;
    let emailWarning = document.getElementById('editEmailWarning') as HTMLElement;

    if (nameInput.value.trim() === '') {
        nameWarning.textContent = 'Please enter a name';
        valid = false;
    } else {
        nameWarning.textContent = '';
    }

    let emailRegex: RegExp = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (emailInput.value.trim() === '') {
        emailWarning.textContent = 'Please enter an email';
        valid = false;
    } else if (!emailRegex.test(emailInput.value)) {
        emailWarning.textContent = 'Please enter a valid email';
        valid = false;
    } else if (await isEmailInUse(emailInput.value) && emailInput.value != email) {
        emailWarning.textContent = 'This email is already in use';
        valid = false;
    } else {
        emailWarning.textContent = '';
    }
    return valid
}

function addConfirmListeners(role: UserRoles) {
    if(role === UserRoles.PlatformAdmin){
        butConfirmEditUser.onclick = async () => {
            if (!await validateModal()) return;
            let user: User = {
                userName: nameInput.value,
                email: emailInput.value,
                permissions: getSelectedPermissions()
            }
            API.updateUser(email, user)
                .then(() => {
                    clearModal();
                    editUserModal.hide();
                    isUserInRole(UserRoles.UserPermission).then(result => {
                        resetCards(id, userRoulette, result);
                    })
                })
        }
    } else if (role === UserRoles.Facilitator){
        butConfirmEditUser.onclick = () => {
            let user: FacilitatorUpdate = {
                userName: nameInput.value,
                email: emailInput.value,
                permissions: [],
                removedProjects: getRemovedProjects(),
                addedProjects: getAddedProjects()
            }
            API.updateFacilitator(email, user)
                .then(() => {
                    clearModal();
                    editUserModal.hide();
                    isUserInRole(UserRoles.UserPermission).then(result => {
                        resetCards(id, userRoulette, result);
                    })
                })
        }
    }
    
}


butCancelEditUserModal.onclick = () => {
    clearModal();
    editUserModal.hide();
}

butCloseEditUserModal.onclick = () => {
    clearModal();
    editUserModal.hide();
}

function getSelectedPermissions(): UserRoles[]{
    let userPermissions = document.getElementById("checkEditUserPermission") as HTMLInputElement;
    let projectPermissions = document.getElementById("checkEditProjectPermission") as HTMLInputElement;
    let statisticPermissions = document.getElementById("checkEditStatisticPermission") as HTMLInputElement;
    let permissions: UserRoles[] = [];
    if(userPermissions.checked){
        permissions.push(UserRoles.UserPermission)
    }
    if(projectPermissions.checked){
        permissions.push(UserRoles.ProjectPermission)
    }
    if(statisticPermissions.checked){
        permissions.push(UserRoles.StatisticPermission)
    }
    permissions.push(UserRoles.PlatformAdmin)
    return permissions
}

function fillModal(email: string){
    let permissionsContainer = document.getElementById("editAdminContainer") as HTMLDivElement
    let projectContainer = document.getElementById("editFacilitatorContainer") as HTMLDivElement
    let removeProjectContainer = document.getElementById("removeProjectContainer") as HTMLDivElement
    API.getUser(email)
        .then(user => {
            nameInput.value = user.userName
            emailInput.value = user.email
            if(user.permissions.includes(UserRoles.PlatformAdmin)){
                permissionsContainer.classList.remove("visually-hidden")
                projectContainer.classList.add("visually-hidden")
                removeProjectContainer.classList.add("visually-hidden")
                loadPermissions(user.permissions);
            } else if(user.permissions.includes(UserRoles.Facilitator)){
                permissionsContainer.classList.add("visually-hidden")
                projectContainer.classList.remove("visually-hidden")
                removeProjectContainer.classList.remove("visually-hidden")
                loadProjects(user.email)
            }
            if(user.permissions.includes(UserRoles.PlatformAdmin)) addConfirmListeners(UserRoles.PlatformAdmin)
            if(user.permissions.includes(UserRoles.Facilitator)) addConfirmListeners(UserRoles.Facilitator)
            editUserModal.show()
        })
}

function clearModal(){
    nameInput.value = "";
    emailInput.value = "";
    loadPermissions([]);
    resetWarnings()
}

function loadProjects(email: string){
    API.getPossibleProjects(email)
        .then(projects => {
            let selectProject = document.getElementById("selectEditProject") as HTMLSelectElement
            fillDropdown(projects, selectProject)
        })
    API.getAssignedProjects(email)
        .then(projects => {
            let dropdown = document.getElementById("selectRemoveProject") as HTMLSelectElement
            fillDropdown(projects, dropdown);
        })
}

function fillDropdown(projects: Project[], dropdown: HTMLSelectElement){
    
    dropdown.options.length = 0
    for(let i = 0; i < projects.length; i++){
        let option = document.createElement("option")
        option.value = projects[i].id.toString()
        option.text = projects[i].mainTheme.subject
        dropdown.add(option)
    }
}

function loadPermissions(permissions: string[]){
    let userPermissions = document.getElementById("checkEditUserPermission") as HTMLInputElement;
    let projectPermissions = document.getElementById("checkEditProjectPermission") as HTMLInputElement;
    let statisticPermissions = document.getElementById("checkEditStatisticPermission") as HTMLInputElement;
    
    userPermissions.checked = permissions.includes(UserRoles.UserPermission);
    projectPermissions.checked = permissions.includes(UserRoles.ProjectPermission);
    statisticPermissions.checked = permissions.includes(UserRoles.StatisticPermission);
    
}

function getRemovedProjects(): number[]{
    let dropdown = document.getElementById("selectRemoveProject") as HTMLSelectElement
    let removedProjects: number[] = []
    for(let i = 0; i < dropdown.options.length; i++){
        if(dropdown.options[i].selected){
            removedProjects.push(parseInt(dropdown.options[i].value))
        }
    }
    return removedProjects;
}

function getAddedProjects(){
    let selectProject = document.getElementById("selectEditProject") as HTMLSelectElement
    let addedProjects: number[] = []
    for(let i = 0; i < selectProject.options.length; i++){
        if(selectProject.options[i].selected){
            addedProjects.push(parseInt(selectProject.options[i].value))
        }
    }
    return addedProjects;
}
