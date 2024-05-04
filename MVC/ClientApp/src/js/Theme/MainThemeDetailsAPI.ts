import {SubTheme} from "../Types/ProjectObjects";

const subThemeContainer = document.getElementById("subThemeContainer") as HTMLTableSectionElement;
const linearFlowBtn = document.getElementById("linearBtn") as HTMLButtonElement;
const circularFlowBtn = document.getElementById("circularBtn") as HTMLButtonElement;
let themeId = Number((document.getElementById("themeId") as HTMLSpanElement).innerText);

function loadSubThemes(id: number) {
    fetch(`/api/MainThemes/${id}/SubThemes`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => showSubThemes(data))
        .catch(error => console.error("Error:", error))
}

function showSubThemes(subThemes: SubTheme[]) {
    subThemeContainer.innerHTML = "";
    subThemes.forEach(theme => addSubTheme(theme));
}

function addSubTheme(theme: SubTheme) {
    subThemeContainer.innerHTML += `<tr>
                                        <td>${theme.id}</td>
                                        <td>${theme.subject}</td>
                                        <td><a href="/SubTheme/Subtheme/${theme.id}"">Details</a></td>
                                     </tr>`
}

function loadLinearFlow(id: number) {
    fetch(`/api/MainThemes/${id}/LinearFlow`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then()
        .catch(error => console.error("Error:", error))
}

function loadCircularFlow(id: number) {
    fetch(`/api/MainThemes/${id}/CircularFlow`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then()
        .catch(error => console.error("Error:", error))
}

window.onload = () => loadSubThemes(themeId)
linearFlowBtn.onclick = () => loadLinearFlow(themeId)
circularFlowBtn.onclick = () => loadCircularFlow(themeId)