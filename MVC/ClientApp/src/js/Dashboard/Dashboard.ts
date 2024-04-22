import * as dashboard from "./API/DashboardAPI";
import "./CreateUserModal";
import {isUserInRole} from "../API/UserAPI";
import {UserRoles} from "./Types/UserTypes";

const userRoulette = document.getElementById("UserRoulette") as HTMLDivElement;

let id: string = document.getElementById("platformId")!.textContent!

isUserInRole(UserRoles.UserPermission).then(result => {
    dashboard.generateUserCards(id, userRoulette, result);
})