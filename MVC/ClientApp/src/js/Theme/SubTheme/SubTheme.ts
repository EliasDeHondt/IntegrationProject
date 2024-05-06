import {loadFlows, showFlows, updateSubTheme} from "./API/SubThemeAPI";
import {showNotificationToast} from "../../Toast/NotificationToast";

const flowContainer = document.getElementById("flowContainer") as HTMLTableSectionElement;
let themeId = Number((document.getElementById("subThemeId") as HTMLSpanElement).innerText);
const saveButton = document.getElementById("btnSaveSubTheme") as HTMLButtonElement;

loadFlows(themeId).then(flows => {
    showFlows(flows);
})

saveButton.onclick = () => {
    let subject = (document.getElementById("inputTitle") as HTMLInputElement).value;
    updateSubTheme(themeId, subject).then(() => showNotificationToast("The sub theme has been successfully updated!"));
}