import {Modal} from "bootstrap";
import * as API from "./API/ProjectStatisticsModalAPI"

const statisticsModal = new Modal(document.getElementById('projectStatisticsModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
})

const divStats = document.getElementById("divStats") as HTMLDivElement;
const btnCloseStatistics = document.getElementById("btnCloseStatistics") as HTMLButtonElement;
const btnPlatformStatistics = document.getElementById("btnPlatformStatistics") as HTMLButtonElement;
const modalTitle = document.getElementById("projectTitle") as HTMLSpanElement;

export function showModal(id: number, title: string, description: string) {
    modalTitle.innerText = title;
    generateData(id, description)
    statisticsModal.show();
}

function generateData(id: number, description: string) {
    divStats.innerHTML = ""
    
    const pDescription = document.createElement('p');
    pDescription.innerText = `Description: ${description}`
    if (description == "")
        pDescription.innerText = `Description: <no project description available>`
    
    const hr = document.createElement('hr');
    hr.classList.add("border-top-2", "border-accent")
    
    const pTotalSubThemes = document.createElement('p');
    API.getProjectSubThemesCount(id).then((count) => pTotalSubThemes.innerText = `Total amount of sub-themes: ${count.toString()}`);
    const pTotalFlows = document.createElement('p');
    API.getProjectFlowsCount(id).then((count) => pTotalFlows.innerText = `Total amount of flows: ${count.toString()}`);
    const pTotalRespondents = document.createElement('p');
    API.getProjectRespondentsCount(id).then((count) => pTotalRespondents.innerText = `Total amount of respondents: ${count.toString()}`);

    divStats.appendChild(pDescription)
    divStats.appendChild(hr)
    divStats.appendChild(pTotalSubThemes)
    divStats.appendChild(pTotalFlows)
    divStats.appendChild(pTotalRespondents)
}

btnCloseStatistics.onclick = () => {
    statisticsModal.hide();
}

btnPlatformStatistics.onclick = () => {
    divStats.innerHTML = ""
    
    API.getPlatformOrganisation(getPlatformId()).then((name) => modalTitle.innerText = `${name}`);
    
    const pTotalRespondents = document.createElement('p');
    API.getPlatformRespondentsCount(getPlatformId()).then((count) => pTotalRespondents.innerText = `Total amount of respondents: ${count.toString()}`)
    
    divStats.appendChild(pTotalRespondents);
    
    statisticsModal.show();
}

function getPlatformId(): number {
    let href = window.location.href;
    let regex = RegExp(/\/SharedPlatform\/Dashboard\/(\d+)/).exec(href);

    if (regex)
        return parseInt(regex[1], 10);
    
    return 0;
}