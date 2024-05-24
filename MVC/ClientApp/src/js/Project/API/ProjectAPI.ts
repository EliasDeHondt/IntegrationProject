import {Project} from "../../Types/ProjectObjects";
import {Flow} from "../../Flow/FlowObjects";
import {showFlows} from "./CreateProjectFlowAPI";
import {initializeDeleteButtons} from "../../CreateFlow/DeleteFlowModal";
import {Note} from "../../Flow/Step/StepObjects";

export function fillExisting(project: Project, inputTitle: HTMLInputElement, inputText: HTMLInputElement): void{
    inputTitle.value = project.title
    inputText.value = project.description
}
//Titel & Text checken
export function CheckNotEmpty(inputTitel: HTMLInputElement,errorMessage: string,errorMsgHTML: string): boolean{
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
export async function SetProject(id: number, title: string, description: string) {
    try{
        const response = await fetch("/api/Projects/UpdateProject/" + id, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                title: title,
                description: description
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
export function getIdProject():number{
    let href = window.location.href;
    let regex = RegExp(/\/Project\/(ProjectPage|Notes)\/(\d+)/).exec(href);

    if (regex) {
        return parseInt(regex[2], 10);
    } else {
        console.error("Project ID not found in URL:", href);
        return 0;
    }
}

export async function getMainThemeId(): Promise<number>{
    return getProjectWithId(getIdProject()).then( project => {
        return project.mainThemeId
    })
}

export async function loadFlowsProject(id: number): Promise<Flow[]> {
    return await fetch(`/api/Projects/GetFlowsForProject/${id}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data
        })
        .catch(error => console.error("Error:", error))
}

export async function createProjectFlow(flowtype:string,projectId: number) {
    try {
        const response = await fetch("/api/Projects/CreateProjectFlow/" + flowtype + "/" + projectId, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            }
        });
        if (response.ok) {
            console.log("Flow made successfully.");
        } else {
            console.error("Failed to make new flow.");
        }
    } catch (error) {
        console.error("Error:", error);
    }
}
export function resetFlowsProject(flows: Flow[], flowcontainer: HTMLDivElement){
    let length = flowcontainer.children.length
    for (let i = length - 1; i > 0; i--){
        flowcontainer.children[i].remove();
    }
    showFlows(flows, "forProject",flowcontainer);
}

export async function loadNotesProject(id: number): Promise<Flow[]> {
    return await fetch(`/api/Projects/GetNotesForProject/${id}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data
        })
        .catch(error => console.error("Error:", error))
}


export async function UpdateProjectClosed(projectId: number,closeProject: boolean) {
    try{
        const response = await fetch("/api/Projects/UpdateProjectClosed/" + projectId + "/" + closeProject, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            }
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

export async function GetProjectClosed(projectId: number) : Promise<boolean> {
    return await fetch(`/api/Projects/GetProjectClosed/${projectId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(boolean => {
            return boolean
        })
        .catch(error => console.error("Error:", error))
}