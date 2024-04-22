import * as dashboard from "./API/DashboardAPI";
import "./CreateUserModal";

const userRoulette = document.getElementById("UserRoulette") as HTMLDivElement;

let id: string = document.getElementById("platformId")!.textContent!

dashboard.generateUserCards(id, userRoulette);