import {
    createSubthemeFlow,
    getStylingTemplate,
    loadFlowsSub,
    resetFlowsSub,
    updateSubTheme
} from "./API/SubThemeAPI";
import {showNotificationToast} from "../../Toast/NotificationToast";
import {GetFlows} from "../../CreateFlow/FlowCreator";
import {showFlows} from "../../Project/API/CreateProjectFlowAPI";
import {Modal, Toast} from "bootstrap";
import {createProjectFlow, getIdProject, loadFlowsProject, resetFlowsProject} from "../../Project/API/ProjectAPI";
import {data} from "@tensorflow/tfjs";

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

document.addEventListener("DOMContentLoaded", function () {
    getStylingTemplate(themeId);
})