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
import {resetCards, resetProjectCards} from "./API/DashboardAPI";
import {UserRoles} from "./Types/UserTypes";

const CreateProjectModal = new Modal(document.getElementById('CreateProjectModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

const projectCreatedToast = new Toast(document.getElementById("projectToast")!);
const projectDescCreatedToast = new Toast(document.getElementById("projectDescriptionToast")!);

const btnCreateProject = document.getElementById("btnCreateProject") as HTMLButtonElement;
const butConfirmCreateProject = document.getElementById("butConfirmCreateProject") as HTMLButtonElement;
const butCloseCreateProjectModal = document.getElementById("butCloseCreateProjectModal") as HTMLButtonElement;
const butCancelCreateProjectModal = document.getElementById("butCancelCreateProjectModal") as HTMLButtonElement;
const inputPName = document.getElementById("inputPName") as HTMLInputElement;
const inputDescription = document.getElementById("inputDescription") as HTMLInputElement;
const pnameWarning = document.getElementById('pnameWarning') as HTMLElement;
const descriptionWarning = document.getElementById('descriptionWarning') as HTMLElement;
const projectRoulette = document.getElementById("carouselContainer") as HTMLDivElement;

let id: string = document.getElementById("platformId")!.textContent!

btnCreateProject.onclick = async () => {
    CreateProjectModal.show();
}
butConfirmCreateProject.onclick = async () => {
    createProjectShow()
}
butCloseCreateProjectModal.onclick = () => {
    clearModal()
}
butCancelCreateProjectModal.onclick = () => {
    clearModal()
}
async function createProjectShow() {
    if (await validateForm()) {
        createProject(inputPName.value, inputDescription.value, 2) // todo: sharedplatform get id sprint 3
            .then(() => clearModal())
            .then(() => {
                projectCreatedToast.show()
                let closeProjectToast = document.getElementById("closeProjectToast") as HTMLButtonElement
                closeProjectToast.onclick = () => projectCreatedToast.hide()
            })
            .finally(() => resetProjectCards(id, projectRoulette))
    }
}
function clearModal() {
    inputPName.value = "";
    inputDescription.value = "";
    resetWarnings();
    CreateProjectModal.hide();
}

function resetWarnings() {
    pnameWarning.textContent = '';
    descriptionWarning.textContent = '';
}

async function validateForm() {
    let valid: boolean = true;

    console.log(inputPName.textContent)
    console.log(inputDescription.value)
    if (inputPName.value.trim() === '') {
        pnameWarning.textContent = 'Please enter a name';
        valid = false;
    } else {
        pnameWarning.textContent = '';
    }
    
    if (inputDescription.value.trim() === '') {
        //toast
        projectDescCreatedToast.show()
        let closeProjectDescToast = document.getElementById("closeProjectDescriptionToast") as HTMLButtonElement
        let addProjectDescriptionToast = document.getElementById("addProjectDescriptionToast") as HTMLButtonElement
        closeProjectDescToast.onclick = () => projectDescCreatedToast.hide()
        addProjectDescriptionToast.onclick = () => {
            projectDescCreatedToast.hide()
            if (inputPName.value.trim() !== '') {
                clearModal()
                createProjectShow()
                valid = false;
            }
        }
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