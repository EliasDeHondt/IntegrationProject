import * as dashboard from "./API/DashboardAPI";
import "./CreateUserModal";
import "./CreateProjectModal";
import "./DeleteUserModal";
import {isUserInRole} from "../API/UserAPI";
import {UserRoles} from "../Types/UserTypes";
import "/node_modules/embla-carousel";
import EmblaCarousel, {EmblaOptionsType} from "embla-carousel";
import {addPrevNextBtnsClickHandlers} from '../EmblaCarouselArrowButtons'
import '../../css/embla.scss'

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

document.addEventListener("DOMContentLoaded", async function() {
    let userPermission = await isUserInRole(UserRoles.UserPermission);
    let projectPermission = await isUserInRole(UserRoles.ProjectPermission);
    let statisticPermission = await isUserInRole(UserRoles.StatisticPermission);
    let systemAdmin = await isUserInRole(UserRoles.SystemAdmin);

    dashboard.generateUserCards(id, userRoulette, userPermission || systemAdmin);
    dashboard.generateProjectCards(id, projectRoulette);
    
})
emblaApiProject.on('destroy', removePrevNextBtnsClickHandlersProject)
emblaApiUser.on('destroy', removePrevNextBtnsClickHandlersUser)
