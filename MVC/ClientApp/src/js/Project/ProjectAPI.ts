import { Project } from "./ProjectObjects";
import {Modal} from "bootstrap";

let inputTitle = (document.getElementById("inputTitle") as HTMLInputElement);
let inputText = (document.getElementById("inputText") as HTMLInputElement);
const btnPublishProject = document.getElementById("btnPublishProject") as HTMLButtonElement;

//Check empty
function isInputEmpty(input: HTMLInputElement): boolean {
    return input.value.trim() === '';
}

//Titel & Text checken
function CheckNotEmpty(inputTitel: HTMLInputElement,errorMessage: string,errorMsgHTML: string): boolean{
    let p = document.getElementById(errorMsgHTML) as HTMLElement;
    console.log(inputTitel.value.trim())
    if (inputTitel.value.trim() === '') {
        p.innerHTML = errorMessage + " can't be empty";
        p.style.color = "red";
        return false;
    } else {
        p.innerHTML = errorMessage + " accepted!";
        p.style.color = "blue";
        return true;
    }
}

//Text checken
function CheckText(inputText: HTMLInputElement): boolean{
    let p = document.getElementById("errorMsgText") as HTMLElement;
    console.log(inputText.value.trim())
    if (inputText.value.trim() === '') {
        p.innerHTML = "Text can't be empty";
        p.style.color = "red";
        return false;
    } else {
        p.innerHTML = "Text accepted!";
        p.style.color = "blue";
        return true;
    }
}
//Project oplsaan (publish)
document.addEventListener("DOMContentLoaded", function () {
    btnPublishProject.onclick = function () {
        console.log("click");
        CheckNotEmpty(inputTitle,"Title","errorMsgTitle");
        CheckNotEmpty(inputTitle,"Text","errorMsgText");
        //CheckText(inputText);
        // @ts-ignore
        // const inputEmail = emailInput.value.trim();
        // const inputElement = emailInput as HTMLInputElement;
        //
        // if (CheckTitel(inputTitle)) {
        //     if (inputEmail !== "") {
        //         //SetRespondentEmail(flowId, inputEmail);
        //
        //         if (inputElement !== null) {
        //             inputElement.value = "";
        //             console.log("Reset value.");
        //         } else {
        //             console.log("No value to reset.");
        //         }
        //     }
        // }
    };
});