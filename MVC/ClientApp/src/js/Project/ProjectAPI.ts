import { Project } from "./Project/ProjectObjects";
import { downloadVideoFromBucket } from "../StorageAPI";
import {Flow} from "./FlowObjects";
import {Modal} from "bootstrap";

const questionContainer = document.getElementById("questionContainer") as HTMLDivElement;
const informationContainer = document.getElementById("informationContainer") as HTMLDivElement;
const btnNextStep = document.getElementById("btnNextStep") as HTMLButtonElement;
const btnRestartFlow = document.getElementById("btnRestartFlow") as HTMLButtonElement;
const btnPauseFlow = document.getElementById("btnPauseFlow") as HTMLButtonElement;
const btnUnPauseFlow = document.getElementById("btnUnPauseFlow") as HTMLButtonElement;
const btnEmail = document.getElementById("btnEmail") as HTMLButtonElement;
const modal = new Modal(document.getElementById("pausedFlowModal") as HTMLDivElement);
let currentStepNumber: number = 0;
let userAnswers: string[] = []; // Array to store user answers
let openUserAnswer: string = "";
let flowId = Number((document.getElementById("flowId") as HTMLSpanElement).innerText);
let themeId = Number((document.getElementById("theme") as HTMLSpanElement).innerText);
let steptotal = Number((document.getElementById("steptotal") as HTMLSpanElement).innerText);
let flowtype = (document.getElementById("flowtype") as HTMLSpanElement).innerText;

//email checken
function CheckEmail(inputEmail: string,inputElement:HTMLInputElement): boolean{
    const emailRegex: RegExp = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    let p = document.getElementById("errorMsg") as HTMLElement;
    if (emailRegex.test(inputEmail)) { // het is een email
        // if (errorMessage && errorMessage.innerHTML === "Not an Email.") {
        //errorMessage.innerHTML = "&nbsp;"
        p.innerHTML = "Email submitted!";
        p.style.color = "blue";
        console.log("nbsp");
        // }
        console.log("mailss")
        return true;
    } else {

        //let p = document.getElementById("errorMsg") as HTMLElement;
        p.innerHTML = "Not an Email.";
        p.style.color = "red";
        // if (!errorMessage) {
        //     // @ts-ignore
        //     inputElement.parentNode.appendChild(p);
        console.log("append chikd")
        // }
        return false;
    }
}