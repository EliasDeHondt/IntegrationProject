import {Project, SharedPlatform} from "./ProjectObjects";
import {Modal} from "bootstrap";
import {MainTheme} from "../Theme/ThemeObjects";
import {Flow} from "../Flow/FlowObjects";
import {generateProjectCard, getProjectsForPlatform} from "../Dashboard/API/DashboardAPI";

let inputTitle = (document.getElementById("inputTitle") as HTMLInputElement);
let inputText = (document.getElementById("inputText") as HTMLInputElement);
const btnPublishProject = document.getElementById("btnPublishProject") as HTMLButtonElement;

function fillExisting(project: Project): void{
    inputTitle.value = project.title //<input id="inputTitle" class="m-1" placeholder="Project Title.">
    inputText.value = project.description //<input id="inputTitle" class="m-1" placeholder="Project Title.">
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

//Project oplsaan (publish)
async function SetProject(mainTheme: string, sharedPlatformid: number) {
    try {
        const response = await fetch("/api/Projects/AddProject/" + mainTheme + "/" + sharedPlatformid.toString(), {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                mainTheme: mainTheme,
                sharedPlatform: sharedPlatformid
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

export async function getProjectWithId(projectId: number): Promise<Project>{
    return await fetch("/api/Projects/GetProjectWithId/" + projectId)
        .then(response => response.json())
        .then(data => {
            return data
        })
}

//Button
document.addEventListener("DOMContentLoaded", async function () {
    // @ts-ignore
    let projectId = document.getElementById('btnPublishProject').getAttribute('data-project-id');
    // @ts-ignore
    const projectIdNumber = parseInt(projectId, 10); // Assuming it's an integer
    try {
        const project = await getProjectWithId(projectIdNumber);
        fillExisting(project);
        console.log(project.id + project.title)
    } catch (error) {
        console.error('Error fetching project:', error);
    }

    btnPublishProject.onclick = function () {
        console.log("click");

        if (CheckNotEmpty(inputTitle, "Title", "errorMsgTitle")) {
            SetProject(inputTitle.value, 2);

            window.location.href = "/SharedPlatform/Dashboard/";
        }
    };
});

//select subthemas todo Matthias