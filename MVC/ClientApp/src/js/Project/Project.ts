import "./CreateSubThemeModal";
import {
    CheckNotEmpty,
    fillExisting,
    getIdProject,
    getProjectWithId,
    SetProject,
    setupsaNavigation
} from "./API/ProjectAPI";
import {generateCards, getSubThemesForProject} from "./API/SubThemeAPI";

let inputTitle = (document.getElementById("inputTitle") as HTMLInputElement);
let inputText = (document.getElementById("inputText") as HTMLInputElement);
const btnPublishProject = document.getElementById("btnPublishProject") as HTMLButtonElement;
const subThemeRoulette = document.getElementById("carouselContainer") as HTMLDivElement;

document.addEventListener("DOMContentLoaded", async function () {
    const projectIdNumber = getIdProject();
    const project = await getProjectWithId(projectIdNumber);
    fillExisting(project, inputTitle, inputText);
    getSubThemesForProject(project.id).then(subThemes => generateCards(subThemes, subThemeRoulette));
    btnPublishProject.onclick = function () {
        if (CheckNotEmpty(inputTitle, "Title", "errorMsgTitle")) {
            SetProject(project.id, inputTitle.value, inputText.value);
        }
    };

    //todo sprint 3: maak api functie die deze ophaalt:
    // let id: string = "2" //document.getElementById("platformId")!.textContent!
    const accountname = document.getElementById("accountname") as HTMLElement;
    //
    // showUserName(id,accountname);
    accountname.textContent = "Welcome " + "Henk" + "!";
    setupsaNavigation();
});