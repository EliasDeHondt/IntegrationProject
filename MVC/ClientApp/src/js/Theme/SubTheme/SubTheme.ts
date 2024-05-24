import {
    createSubthemeFlow,
    getProjectId,
    loadFlowsSub,
    resetFlowsSub,
    updateSubTheme
} from "./API/SubThemeAPI";
import {showNotificationToast} from "../../Toast/NotificationToast";
import {GetFlows} from "../../CreateFlow/FlowCreator";
import {showFlows} from "../../Project/API/CreateProjectFlowAPI";
import {Modal, Toast} from "bootstrap";
import {createProjectFlow, getIdProject, loadFlowsProject, resetFlowsProject} from "../../Project/API/ProjectAPI";

const flowContainer = document.getElementById("flow-cards") as HTMLTableSectionElement;
let themeId = Number((document.getElementById("subThemeId") as HTMLSpanElement).innerText);
const saveButton = document.getElementById("btnSaveSubTheme") as HTMLButtonElement;
const btnCreateFlowSub = document.getElementById("btnCreateFlowSub") as HTMLButtonElement;

const subFlowToast = new Toast(document.getElementById("subFlowToast")!);


loadFlowsSub(themeId).then(flows => {
    showFlows(flows,"forSubtheme",flowContainer);
})

saveButton.onclick = () => {
    let subject = (document.getElementById("inputTitle") as HTMLInputElement).value;
    updateSubTheme(themeId, subject).then(() => showNotificationToast("The sub theme has been successfully updated!"));
}


btnCreateFlowSub.onclick = async() => {
    function reset() {
        loadFlowsSub(themeId)
            .then(flows => resetFlowsSub(flows, flowContainer));
    }
    
    let flowtype = "Linear"

    createSubthemeFlow(flowtype,themeId).then(() => {
    }).then(() =>{
        reset();
    }).then(() => subFlowToast.show());
    
    let closeSubFlowToast = document.getElementById("closeSubFlowToast") as HTMLButtonElement
    closeSubFlowToast.onclick = () => subFlowToast.hide()
    
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

document.addEventListener("DOMContentLoaded", function () {
    console.log(getProjectId(themeId));
})