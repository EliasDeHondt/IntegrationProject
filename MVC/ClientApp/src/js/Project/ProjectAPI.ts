import { Project } from "./ProjectObjects";
import {Modal} from "bootstrap";

let inputTitle = (document.getElementById("inputTitle") as HTMLInputElement);
let inputText = (document.getElementById("inputText") as HTMLInputElement);
const btnPublishProject = document.getElementById("btnPublishProject") as HTMLButtonElement;

//Check empty
function isInputEmpty(input: HTMLInputElement): boolean {
    return input.value.trim() === '';
}

//Titel checken
function CheckTitel(inputTitel: HTMLInputElement): boolean{
    let p = document.getElementById("errorMsgTitle") as HTMLElement;
    if (inputTitel.value.trim() === '') { // het is een email
        p.innerHTML = "Title accepted!";
        p.style.color = "blue";
        return true;
    } else {
        p.innerHTML = "Title can't be empty";
        p.style.color = "red";
        console.log("append chikd")
        return false;
    }
}

//Text checken
function CheckText(inputText: string,inputElement:HTMLInputElement): boolean{
    const emailRegex: RegExp = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    let p = document.getElementById("errorMsgText") as HTMLElement;
    if (emailRegex.test(inputText)) { // het is een email
        p.innerHTML = "Title accepted!";
        p.style.color = "blue";
        return true;
    } else {
        p.innerHTML = "Title can't be empty";
        p.style.color = "red";
        console.log("append chikd")
        return false;
    }
}
//Project oplsaan (publish)
document.addEventListener("DOMContentLoaded", function () {
    const emailInput = document.getElementById("inputEmail");

    btnPublishProject.onclick = function () {
        console.log("click");
        // @ts-ignore
        const inputEmail = emailInput.value.trim();
        const inputElement = emailInput as HTMLInputElement;

        if (CheckTitel(inputTitle)) {
            if (inputEmail !== "") {
                //SetRespondentEmail(flowId, inputEmail);

                if (inputElement !== null) {
                    inputElement.value = "";
                    console.log("Reset value.");
                } else {
                    console.log("No value to reset.");
                }
            }
        }
    };
});