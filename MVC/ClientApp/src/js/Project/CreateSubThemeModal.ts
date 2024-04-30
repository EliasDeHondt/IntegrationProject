import { Modal } from 'bootstrap'

import EmblaCarousel, {EmblaOptionsType} from "embla-carousel";
import {addPrevNextBtnsClickHandlers} from "../EmblaCarouselArrowButtons";
import {createSubTheme} from "./API/CreateSubThemeAPI";
import {getMainThemeId} from "./API/ProjectAPI";

const OPTIONS: EmblaOptionsType = {align: 'start'};
const emblaNodeSubTheme = <HTMLElement>document.getElementById('emblaSubThemes');
const viewportNodeSubTheme = <HTMLElement>emblaNodeSubTheme.querySelector('.embla__viewport');
const prevBtnNodeSubTheme = <HTMLElement>emblaNodeSubTheme.querySelector('.embla__button--prev');
const nextBtnNodeSubTheme = <HTMLElement>emblaNodeSubTheme.querySelector('.embla__button--next');
const emblaApiSubTheme = EmblaCarousel(viewportNodeSubTheme, OPTIONS);
const removePrevNextBtnsClickHandlersUser = addPrevNextBtnsClickHandlers(
    emblaApiSubTheme,
    prevBtnNodeSubTheme,
    nextBtnNodeSubTheme
);

const subThemeModal = new Modal(document.getElementById('createSubThemeModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});
const butCreateSubTheme = document.getElementById('butConfirmCreateSubTheme') as HTMLButtonElement;
const butCancelCreateSubTheme = document.getElementById('butCancelCreateSubTheme') as HTMLButtonElement;
const butCloseCreateSubThemeModal = document.getElementById('butCloseCreateSubTheme') as HTMLButtonElement;
const btnOpenModal = document.getElementById("btnCreateSubTheme") as HTMLButtonElement;
const inputSTSubject = document.getElementById("inputSTSubject") as HTMLInputElement;

btnOpenModal.onclick = function () {
    subThemeModal.show();

    butCreateSubTheme.onclick = function () {
        getMainThemeId().then(id => {
            createSubTheme(inputSTSubject.value, id).then(() =>{
                subThemeModal.hide()
            });
        })
    }

    butCancelCreateSubTheme.onclick = function () {
        subThemeModal.hide();
    };
    butCloseCreateSubThemeModal.onclick = function () {
        subThemeModal.hide();
    };
}

emblaApiSubTheme.on("destroy", removePrevNextBtnsClickHandlersUser);