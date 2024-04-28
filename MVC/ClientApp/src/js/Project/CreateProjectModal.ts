import {Modal} from "bootstrap";

const btnCreateProject = document.getElementById("btnCreateProject") as HTMLButtonElement;
const butCloseCreateProjectModal = document.getElementById("butCloseCreateProjectModal") as HTMLButtonElement;
const inputName = document.getElementById("inputName") as HTMLInputElement;
const inputDescription = document.getElementById("inputDescription") as HTMLInputElement;

const CreateProjectModal = new Modal(document.getElementById('CreateProjectModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

btnCreateProject.onclick = () => {
    CreateProjectModal.show();
}
//
// butCloseCreateProjectModal.onclick = () => {
//     clearModal()
// }
function clearModal() {
    inputName.value = "";
    inputDescription.value = "";
    CreateProjectModal.hide();
}