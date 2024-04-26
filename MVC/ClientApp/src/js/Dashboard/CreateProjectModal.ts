// import {Modal} from "bootstrap";
//
// const btnCreateProject = document.getElementById("btnCreateProject") as HTMLButtonElement;
// const butCloseCreateProjectModal = document.getElementById("butCloseCreateProjectModal") as HTMLButtonElement;
// const inputName = document.getElementById("inputPName") as HTMLInputElement;
// const inputDescription = document.getElementById("inputDescription") as HTMLInputElement;
//
// const CreateProjectModal = new Modal(document.getElementById('CreateProjectModal')!, {
//     keyboard: false,
//     focus: true,
//     backdrop: "static"
// });
//
// btnCreateProject.onclick = () => {
//     CreateProjectModal.show();
// }
//
// butCloseCreateProjectModal.onclick = () => {
//     clearModal()
// }
// function clearModal() {
//     inputName.value = "";
//     inputDescription.value = "";
//     CreateProjectModal.hide();
// }
import {Modal, Toast} from "bootstrap";
import {isEmailInUse} from "../API/UserAPI";
import * as API from "./API/CreateUserModalAPI";
import {resetCards} from "./API/DashboardAPI";
import {UserRoles} from "./Types/UserTypes";

const CreateProjectModal = new Modal(document.getElementById('CreateProjectModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

const projectCreatedToast = new Toast(document.getElementById("projectToast")!);

const btnCreateProject = document.getElementById("btnCreateProject") as HTMLButtonElement;
const butConfirmCreateProject = document.getElementById("butConfirmCreateProject") as HTMLButtonElement;
const butCloseCreateProjectModal = document.getElementById("butCloseCreateProjectModal") as HTMLButtonElement;
const butCancelCreateProjectModal = document.getElementById("butCancelCreateProjectModal") as HTMLButtonElement;
const inputName = document.getElementById("inputPName") as HTMLInputElement;
const inputDescription = document.getElementById("inputDescription") as HTMLInputElement;
const nameWarning = document.getElementById('nameWarning') as HTMLElement;
const descriptionWarning = document.getElementById('descriptionWarning') as HTMLElement;
const projectRoulette = document.getElementById("carouselContainer") as HTMLDivElement;

let id: string = document.getElementById("platformId")!.textContent!

btnCreateProject.onclick = async () => {
    //if (await validateForm()) {
        CreateProjectModal.show();
 //   }
}
butConfirmCreateProject.onclick = async () => {
    if (await validateForm()) {
        createProject(inputName.value, inputDescription.value,2) // todo: sharedplatform get id sprint 3
            .then(() => clearModal())
            .then(() => {
                projectCreatedToast.show()
                let closeUserToast = document.getElementById("closeProjectToast") as HTMLButtonElement
                closeUserToast.onclick = () => projectCreatedToast.hide()
            })
            .finally(() => resetCards(id, projectRoulette, true))
    }
}
butCloseCreateProjectModal.onclick = () => {
    clearModal()
}
butCancelCreateProjectModal.onclick = () => {
    clearModal()
}

function clearModal() {
    inputName.value = "";
    inputDescription.value = "";
    resetWarnings();
    CreateProjectModal.hide();
}

function resetWarnings() {
    nameWarning.textContent = '';
    descriptionWarning.textContent = '';
}

async function validateForm() {
    let valid: boolean = true;

    if (inputName.value.trim() === '') {
        nameWarning.textContent = 'Please enter a name';
        valid = false;
    } else {
        nameWarning.textContent = '';
    }
    
    if (inputDescription.value.trim() === '') {
        descriptionWarning.textContent = 'Please enter a description';
        valid = false;
    } else {
        descriptionWarning.textContent = '';
    }

    return valid;
}

export async function createProject(name: string, description:string, platform: number) {
    await fetch("/api/Projects/AddProject", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: name,
            description: description,
            sharedplatformId: platform
        })
    })
}