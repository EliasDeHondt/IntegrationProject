import {loadFlows, showFlows, updateSubTheme} from "./API/SubThemeAPI";

const flowContainer = document.getElementById("flowContainer") as HTMLTableSectionElement;
let themeId = Number((document.getElementById("subThemeId") as HTMLSpanElement).innerText);
const saveButton = document.getElementById("btnSaveSubTheme") as HTMLButtonElement;

loadFlows(themeId).then(flows => {
    showFlows(flows, flowContainer);
})

saveButton.onclick = async () => {
    let subject = (document.getElementById("inputTitle") as HTMLInputElement).value;
    await updateSubTheme(themeId, subject);
}