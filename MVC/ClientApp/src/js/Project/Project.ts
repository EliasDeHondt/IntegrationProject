﻿import "./CreateSubThemeModal";
import {CheckNotEmpty, fillExisting, getIdProject, getProjectWithId, SetProject} from "./API/ProjectAPI";
import {generateCards, getSubThemesForProject} from "./API/SubThemeAPI";

let inputTitle = (document.getElementById("inputTitle") as HTMLInputElement);
let inputText = (document.getElementById("inputText") as HTMLInputElement);
const btnPublishProject = document.getElementById("btnPublishProject") as HTMLButtonElement;

document.addEventListener("DOMContentLoaded", async function () {
    const projectIdNumber = getIdProject();
    const project = await getProjectWithId(projectIdNumber);
    fillExisting(project, inputTitle, inputText);
    getSubThemesForProject(project.id).then(subThemes => generateCards(subThemes));
    btnPublishProject.onclick = function () {
        if (CheckNotEmpty(inputTitle, "Title", "errorMsgTitle")) {
            SetProject(project.id, inputTitle.value, inputText.value);
        }
    };
});