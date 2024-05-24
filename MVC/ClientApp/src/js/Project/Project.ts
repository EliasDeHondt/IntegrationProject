import "./CreateSubThemeModal";
import "./DeleteSubThemeModal"
import {
    CheckNotEmpty, createProjectFlow,
    fillExisting,
    getIdProject,
    getProjectWithId, loadFlowsProject, resetFlowsProject,
    SetProject,
} from "./API/ProjectAPI";
import {generateCards, getSubThemesForProject, resetCards} from "./API/SubThemeAPI";
import {Modal, Toast} from "bootstrap";
import {showFlows} from "./API/CreateProjectFlowAPI";

let inputTitle = (document.getElementById("inputTitle") as HTMLInputElement);
let inputText = (document.getElementById("inputText") as HTMLInputElement);
const btnPublishProject = document.getElementById("btnPublishProject") as HTMLButtonElement;
const subThemeRoulette = document.getElementById("carouselContainer") as HTMLDivElement;
// load flows for project
const parts = window.location.pathname.split('/');
const projectIdString = parts[parts.length - 1]; //laatste
const projectId = parseInt(projectIdString, 10);

const projectFlowToast = new Toast(document.getElementById("projectFlowToast")!);
const toastBody = document.getElementById('toastprojtext') as HTMLDivElement;

const flowContainer = document.getElementById("flow-cards") as HTMLDivElement;
const btnCreateFlowProject = document.getElementById("btnCreateFlowProject") as HTMLButtonElement;
const butCancelCreateprojFlow = document.getElementById('butCancelCreateprojFlow') as HTMLButtonElement;
const butCloseCreateprojFlow = document.getElementById('butCloseCreateprojFlow') as HTMLButtonElement;
const butConfirmCreateprojFlow = document.getElementById('butConfirmCreateprojFlow') as HTMLButtonElement;

const closedProjectOverlay = document.getElementById("closedProjectOverlay") as HTMLDivElement;

let deleteButtons: NodeListOf<HTMLButtonElement>;
let id: number;
const butConfirmDeleteFlow = document.getElementById("btnConfirmDeleteFlow") as HTMLButtonElement
const butCancelDeleteFlowModal = document.getElementById("btnCancelDeleteFlowModal") as HTMLButtonElement

const linear = document.getElementById("linear") as HTMLInputElement;
const circular = document.getElementById("circular") as HTMLInputElement;
const projFlowModal = new Modal(document.getElementById('createprojFlowModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

loadFlowsProject(getIdProject()).then(flows => {
    showFlows(flows,"forProject",flowContainer);
})

btnCreateFlowProject.onclick = async() => {
    projFlowModal.show();
    function reset() {
        loadFlowsProject(getIdProject())
            .then(flows => resetFlowsProject(flows, flowContainer));
    }
    butConfirmCreateprojFlow.onclick = async() => {
        let checked = false;
        let flowtype = ""
        if(linear.checked){
            flowtype = "Linear"
            checked = true;
        }else if(circular.checked){
            flowtype = "Circular"
            checked = true;
        }
        if (checked) {
            toastBody.textContent = "New flow has been created!";
            createProjectFlow(flowtype,getIdProject()).then(() => {
            }).then(() =>{
                projFlowModal.hide();
                reset();
            }).then(() => projectFlowToast.show());
            
        } else {
            toastBody.textContent = "Please select a flow type.";
            projectFlowToast.show()
            projFlowModal.show();
        }
        
        let closeProjectFlowToast = document.getElementById("closeProjectFlowToast") as HTMLButtonElement
        closeProjectFlowToast.onclick = () => projectFlowToast.hide()
    }
    butCancelCreateprojFlow.onclick = function () {
        projFlowModal.hide();
    };
    butCloseCreateprojFlow.onclick = function () {
        projFlowModal.hide();
    };
}

function projectOverlay(isVisible: boolean) {
    if (isVisible) {
        closedProjectOverlay.style.display = isVisible ? 'block' : 'none';
    }
}

document.addEventListener("DOMContentLoaded", async function () {
    projectOverlay(false);
    
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

