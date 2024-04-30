import * as dashboard from "./API/DashboardAPI";
import "./CreateUserModal";
import "./CreateProjectModal";
import "./DeleteUserModal";
import {isUserInRole} from "../API/UserAPI";
import {UserRoles} from "./Types/UserTypes";
import "/node_modules/embla-carousel";
import EmblaCarousel, {EmblaOptionsType} from "embla-carousel";
import {addPrevNextBtnsClickHandlers} from './EmblaCarouselArrowButtons'
import '../../css/embla.scss'
import {showUserName} from "./API/DashboardAPI";

const accountname = document.getElementById("accountname") as HTMLElement;
const userRoulette = document.getElementById("carouselContainer") as HTMLDivElement;
const projectRoulette = document.getElementById("carouselPContainer") as HTMLDivElement;
const OPTIONS: EmblaOptionsType = {align: 'start'};

//User Carousel
const emblaNodeUser = <HTMLElement>document.getElementById('emblaUser');
const viewportNodeUser = <HTMLElement>emblaNodeUser.querySelector('.embla__viewport');
const prevBtnNodeUser = <HTMLElement>emblaNodeUser.querySelector('.embla__button--prev');
const nextBtnNodeUser = <HTMLElement>emblaNodeUser.querySelector('.embla__button--next');
const emblaApiUser = EmblaCarousel(viewportNodeUser, OPTIONS);
const removePrevNextBtnsClickHandlersUser = addPrevNextBtnsClickHandlers(
    emblaApiUser,
    prevBtnNodeUser,
    nextBtnNodeUser
);

//Project Carousel
const emblaNodeProject = <HTMLElement>document.getElementById('emblaProject');
const viewportNodeProject = <HTMLElement>emblaNodeProject.querySelector('.embla__viewport');
const prevBtnNodeProject = <HTMLElement>emblaNodeProject.querySelector('.embla__button--prev');
const nextBtnNodeProject = <HTMLElement>emblaNodeProject.querySelector('.embla__button--next');
const emblaApiProject = EmblaCarousel(viewportNodeProject, OPTIONS);
const removePrevNextBtnsClickHandlersProject = addPrevNextBtnsClickHandlers(
    emblaApiProject,
    prevBtnNodeProject,
    nextBtnNodeProject
);


let id: string = document.getElementById("platformId")!.textContent!

isUserInRole(UserRoles.UserPermission).then(result => {
    dashboard.showUserName(id,accountname);
    dashboard.generateUserCards(id, userRoulette, result);
    dashboard.generateProjectCards(id, projectRoulette);
})
emblaApiProject.on('destroy', removePrevNextBtnsClickHandlersProject)
emblaApiUser.on('destroy', removePrevNextBtnsClickHandlersUser)


document.addEventListener('DOMContentLoaded', function() {
    const navigationBar = document.getElementById('navbar') as HTMLElement;
    const mainContent = document.getElementById('maincontent') as HTMLElement;
    const collapseButton = document.getElementById('collapseButton') as HTMLElement;
    const icon = document.getElementById('icon') as HTMLElement;
    
    collapseButton.addEventListener('click', function(e) {
        e.preventDefault();
        //navigationBar.classList.toggle('collapsed');

        // Toggle icon
        icon.classList.toggle('bi-caret-left-fill');
        icon.classList.toggle('bi-caret-right-fill');
    });
    
    // collapseButton.addEventListener('click', function() {
    //     if (navigationBar.classList.contains('collapsed')) {
    //         mainContent.style.paddingRight = '50px'; // Adjust to the width of the collapsed navigation bar
    //     } else {
    //         mainContent.style.paddingRight = '800px'; // Adjust to the width of the expanded navigation bar
    //     }
    // });
});