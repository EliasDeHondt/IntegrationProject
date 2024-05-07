import {loadFlows, updateSubTheme} from "./API/SubThemeAPI";
import {showNotificationToast} from "../../Toast/NotificationToast";
import {GetFlows} from "../../CreateFlow/FlowCreator";
import {showFlows} from "../../Project/API/CreateProjectFlowAPI";

const flowContainer = document.getElementById("flow-cards") as HTMLTableSectionElement;
let themeId = Number((document.getElementById("subThemeId") as HTMLSpanElement).innerText);
const saveButton = document.getElementById("btnSaveSubTheme") as HTMLButtonElement;
const btnCreateFlowSub = document.getElementById("btnCreateFlowSub") as HTMLButtonElement;


loadFlows(themeId).then(flows => {
    showFlows(flows,"forSubtheme",flowContainer);
})

saveButton.onclick = () => {
    let subject = (document.getElementById("inputTitle") as HTMLInputElement).value;
    updateSubTheme(themeId, subject).then(() => showNotificationToast("The sub theme has been successfully updated!"));
}

btnCreateFlowSub.onclick = () => {
console.log("click")
    let flowType = "Linear";

    try {
        const response = fetch("/api/SubThemes/CreateSubthemeFlow/" + flowType, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            }
        });

    } catch (error) {
        console.error("Error:", error);
    }

    loadFlows(themeId)
}