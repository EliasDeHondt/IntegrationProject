import "./CreateSubThemeModal";
import {
    CheckNotEmpty, createProjectFlow,
    fillExisting,
    getIdProject,
    getProjectWithId, loadFlowsProject,
    SetProject,
} from "./API/ProjectAPI";
import {generateCards, getSubThemesForProject} from "./API/SubThemeAPI";
import {loadFlows, showFlows} from "../Theme/SubTheme/API/SubThemeAPI";
import {GetFlows} from "../CreateFlow/FlowCreator";
import {Modal} from "bootstrap";

let inputTitle = (document.getElementById("inputTitle") as HTMLInputElement);
let inputText = (document.getElementById("inputText") as HTMLInputElement);
const btnPublishProject = document.getElementById("btnPublishProject") as HTMLButtonElement;
const subThemeRoulette = document.getElementById("carouselContainer") as HTMLDivElement;
// load flows for project
const parts = window.location.pathname.split('/');
const projectIdString = parts[parts.length - 1]; //laatste
const projectId = parseInt(projectIdString, 10);
loadFlowsProject(projectId).then(flows => {
    showFlows(flows,"forProject");
})

const btnCreateFlowProject = document.getElementById("btnCreateFlowProject") as HTMLButtonElement;
const butCancelCreateprojFlow = document.getElementById('butCancelCreateprojFlow') as HTMLButtonElement;
const butCloseCreateprojFlow = document.getElementById('butCloseCreateprojFlow') as HTMLButtonElement;

const projFlowModal = new Modal(document.getElementById('createprojFlowModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

btnCreateFlowProject.onclick = async() => {
    projFlowModal.show();
    createProjectFlow(projectId);
}
butCancelCreateprojFlow.onclick = function () {
    //inputSTSubject.value = '';
    projFlowModal.hide();
};
butCloseCreateprojFlow.onclick = function () {
    projFlowModal.hide();
};

document.addEventListener("DOMContentLoaded", async function () {
    const projectIdNumber = getIdProject();
    const project = await getProjectWithId(projectIdNumber);
    fillExisting(project, inputTitle, inputText);
    getSubThemesForProject(project.id).then(subThemes => generateCards(subThemes, subThemeRoulette));
    btnPublishProject.onclick = function () {
        if (CheckNotEmpty(inputTitle, "Title", "errorMsgTitle")) {
            SetProject(project.id, inputTitle.value, inputText.value);
        }
    }
    
});

