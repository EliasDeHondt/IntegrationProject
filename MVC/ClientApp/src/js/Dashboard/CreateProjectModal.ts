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
import {Modal} from "bootstrap";

const CreateProjectModal = new Modal(document.getElementById('CreateProjectModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

const btnCreateProject = document.getElementById("btnCreateProject") as HTMLButtonElement;
const butCloseCreateProjectModal = document.getElementById("butCloseCreateProjectModal") as HTMLButtonElement;
const inputName = document.getElementById("inputPName") as HTMLInputElement;
const inputDescription = document.getElementById("inputDescription") as HTMLInputElement;

btnCreateProject.onclick = () => {
    CreateProjectModal.show();
}

butCloseCreateProjectModal.onclick = () => {
    clearModal()
}

// butCancelCreateUserModal.onclick = () => {
//     clearModal()
// }

// butConfirmCreateUser.onclick = async (ev) => {
//     ev.preventDefault()
//     // API call to create user
//     if (await validateForm()) {
//         if (radioFacilitator.checked) createFacilitator()
//         else if (radioAdmin.checked) createAdmin()
//     }
// }

// radioAdmin.onchange = () => {
//     facilitatorContainer.classList.add("visually-hidden");
//     selectProject.options.length = 0;
// }
//
// radioFacilitator.onchange = () => {
//
//     facilitatorContainer.classList.remove("visually-hidden");
//     // Generate options for each Project
//     getProjects();
//
// }

function clearModal() {
    inputName.value = "";
    inputDescription.value = "";
    //resetWarnings();
    CreateProjectModal.hide();
}

// function createAdmin() {
//     fetch("/api/Users/CreateAdmin", {
//         method: "POST",
//         headers: {
//             "Content-Type": "application/json"
//         },
//         body: JSON.stringify({
//             name: inputName.value,
//             email: inputEmail.value,
//             password: inputPassword.value,
//             platformId: id
//         })
//     })
//         .then(() => clearModal())
//         .then(() => {
//             userCreatedToast.show()
//             let closeUserToast = document.getElementById("closeUserToast") as HTMLButtonElement
//             closeUserToast.onclick = () => userCreatedToast.hide()
//         })
// }


// function resetWarnings() {
//     nameWarning.textContent = '';
//     emailWarning.textContent = '';
//     passwordWarning.textContent = '';
// }