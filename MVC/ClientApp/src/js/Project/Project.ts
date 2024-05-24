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
import {loadFlowsSub} from "../Theme/SubTheme/API/SubThemeAPI";
import {GetFlows} from "../CreateFlow/FlowCreator";
import {Modal, Toast} from "bootstrap";
import {deleteFlow} from "../CreateFlow/API/DeleteFlowAPI";
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

const btnOpenStyling = document.getElementById('btn-open-styling') as HTMLAnchorElement;

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

async function checkFlowsNotEmpty() {
    //try {
    //     const flows = await loadFlowsProject(getIdProject());
    //     const deleteFlowModalElement = document.getElementById("deleteFlowModal");
    //     if (deleteFlowModalElement) { //niet leeg
    //         const deleteFlowModal = new Modal(deleteFlowModalElement, {
    //             keyboard: false,
    //             focus: true,
    //             backdrop: "static"
    //         });
    //        
    //         butConfirmDeleteFlow.onclick = () => {
    //             deleteFlow(id)
    //                 .then(() => {
    //                     GetFlows(0);
    //                     deleteFlowModal.hide();
    //                 });
    //         }
    //
    //         butCancelDeleteFlowModal.onclick = () => {
    //             deleteFlowModal.hide();
    //         }
    //     } else {
    //         console.log("Flows array is empty");
    //     }
    // } catch (error) {
    //     console.error("Error loading flows:", error);
    // }
}

function getTemplate(projectId:number) {
    fetch(`/Project/GetStylingTemplate/${projectId}`)
        .then(response => response.json())
        .then(data => {
            if (data) {
                document.documentElement.style.setProperty('--primary-color', data.customPrimaryColor);
                document.documentElement.style.setProperty('--secondary-color', data.customSecondaryColor);
                document.documentElement.style.setProperty('--accent-color', data.customAccentColor);
                document.documentElement.style.setProperty('--background-color', data.customBackgroundColor)
            }
        })
        .catch(error => console.error('Error fetching styling template:', error));
}

document.addEventListener("DOMContentLoaded", async function () {
    // loadFlowsProject(getIdProject()).then(flows => {
    //     showFlows(flows,"forProject");
    // })
    checkFlowsNotEmpty()
    
    const projectIdNumber = getIdProject();
    const project = await getProjectWithId(projectIdNumber);
    getTemplate(projectIdNumber);
    fillExisting(project, inputTitle, inputText);
    getSubThemesForProject(project.id).then(subThemes => generateCards(subThemes, subThemeRoulette));
    btnPublishProject.onclick = function () {
        if (CheckNotEmpty(inputTitle, "Title", "errorMsgTitle")) {
            SetProject(project.id, inputTitle.value, inputText.value);
        }
    }
    
    btnOpenStyling.href = "/Project/Styling/" + projectId;
    
});
