import {Project, SharedPlatform} from "./ProjectObjects";
import {Modal} from "bootstrap";
import {MainTheme} from "../Theme/ThemeObjects";
import {Flow} from "../Flow/FlowObjects";

let inputTitle = (document.getElementById("inputTitle") as HTMLInputElement);
let inputText = (document.getElementById("inputText") as HTMLInputElement);
const btnPublishProject = document.getElementById("btnPublishProject") as HTMLButtonElement;

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

//Project oplsaan (publish)
async function SetProject(mainTheme: MainTheme, sharedPlatform: SharedPlatform) {
    try {
        const response = await fetch("/api/Projects/SetProject/" + mainTheme + "/" + sharedPlatform, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                mainTheme: mainTheme,
                sharedPlatform: sharedPlatform
            })
        });
        if (response.ok) {
            console.log("Project saved successfully.");
        } else {
            console.error("Failed to save Project.");
        }
    } catch (error) {
        console.error("Error:", error);
    }
}

//Button
document.addEventListener("DOMContentLoaded", function () {
    btnPublishProject.onclick = function () {
        console.log("click");
        //CheckNotEmpty(inputTitle,"Title","errorMsgTitle");
        //CheckNotEmpty(inputText,"Text","errorMsgText");

        if (CheckNotEmpty(inputTitle,"Title","errorMsgTitle")) {
            const mainTheme: MainTheme = {
                id: 1,
                subject: inputTitle.value,
                flows: [/* todo flow */],
                themes: [/* todo theme */]
            };
            const sharedPlatform: SharedPlatform = {
                id: 1,
                OrganisationName: "test",
                Projects: [/* todo projects */]
            };
            SetProject(mainTheme, sharedPlatform);
        }
                
    };
});


//select subthemas