import * as dashboard from "./API/DashboardAPI";
import "./CreateUserModal";
import "./CreateProjectModal";
import {isUserInRole} from "../API/UserAPI";
import {UserRoles} from "./Types/UserTypes";
import "/node_modules/embla-carousel";
import EmblaCarousel, {EmblaOptionsType} from "embla-carousel";
import { addPrevNextBtnsClickHandlers } from './EmblaCarouselArrowButtons'
import '../../css/embla.scss'

const userRoulette = document.getElementById("carouselContainer") as HTMLDivElement;
const OPTIONS: EmblaOptionsType = { align: 'start' }
const emblaNode = <HTMLElement>document.querySelector('.embla')
const viewportNode = <HTMLElement>emblaNode.querySelector('.embla__viewport')
const prevBtnNode = <HTMLElement>emblaNode.querySelector('.embla__button--prev')
const nextBtnNode = <HTMLElement>emblaNode.querySelector('.embla__button--next')
const emblaApi = EmblaCarousel(viewportNode, OPTIONS)
const removePrevNextBtnsClickHandlers = addPrevNextBtnsClickHandlers(
    emblaApi,
    prevBtnNode,
    nextBtnNode
)

let id: string = document.getElementById("platformId")!.textContent!

isUserInRole(UserRoles.UserPermission).then(result => {
    dashboard.generateUserCards(id, userRoulette, result);
    dashboard.generateProjectCards();
})

emblaApi.on('destroy', removePrevNextBtnsClickHandlers)