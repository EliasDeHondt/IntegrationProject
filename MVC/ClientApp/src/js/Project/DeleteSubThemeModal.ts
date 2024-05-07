import {Modal} from "bootstrap";
import {deleteSubTheme} from "./API/DeleteSubThemeAPI";
import {getIdProject} from "./API/ProjectAPI";
import {getSubThemesForProject, resetCards} from "./API/SubThemeAPI";

export const subThemeModal = new Modal(document.getElementById('deleteSubThemeModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

const btnDeleteSubTheme = document.getElementById('butConfirmDeleteSubTheme') as HTMLButtonElement;
const btnCancelDeleteSubTheme = document.getElementById('butCancelDeleteSubTheme') as HTMLButtonElement;
const butCloseDeleteSubThemeModal = document.getElementById('butCloseDeleteSubTheme') as HTMLButtonElement;
const subThemeRoulette = document.getElementById("carouselContainer") as HTMLDivElement;

btnDeleteSubTheme.onclick = function () {
    deleteSubTheme(parseInt(btnDeleteSubTheme.dataset.subthemeId as string))
        .then(() => {
            reset();
            subThemeModal.hide();
        })
}

function reset() {
    getSubThemesForProject(getIdProject())
        .then(subThemes => resetCards(subThemes, subThemeRoulette));
}

btnCancelDeleteSubTheme.onclick = () => {
    subThemeModal.hide();
}

butCloseDeleteSubThemeModal.onclick = () => {
    subThemeModal.hide();
}