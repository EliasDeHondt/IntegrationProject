import {Project} from "../../Types/ProjectObjects";
import {Flow} from "../../Flow/FlowObjects";

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
    let regex = RegExp(/\/Project\/ProjectPage\/(\d+)/).exec(href);

    if (regex) {
        return parseInt(regex[1], 10);
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

export async function createProjectFlow(projectId: number) {
    try {
        const response = await fetch("/api/Projects/CreateProjectFlow/" + projectId, {
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
    loadFlowsProject(projectId)
}