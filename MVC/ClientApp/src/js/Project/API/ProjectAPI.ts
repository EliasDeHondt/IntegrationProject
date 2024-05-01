import {Project} from "../Types/ProjectObjects";

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

//Nav
export function setupsaNavigation() {
    const navigationBar = document.getElementById('navbar') as HTMLElement;
    const mainContent = document.getElementById('maincontent') as HTMLElement;
    const collapseButton = document.getElementById('collapseButton') as HTMLElement;
    const languageButton = document.getElementById('language') as HTMLElement;
    const accountname = document.getElementById('tohideAN') as HTMLElement;
    const dashboard = document.getElementById('tohideD') as HTMLElement;
    const statistics = document.getElementById('tohideS') as HTMLElement;
    const signout = document.getElementById('tohideLO') as HTMLElement;
    const icon = document.getElementById('icon') as HTMLElement;

    collapseButton.addEventListener('click', function(e) {
        e.preventDefault();
        navigationBar.classList.toggle('collapsed');

        // Toggle icon
        icon.classList.toggle('bi-caret-left-fill');
        icon.classList.toggle('bi-caret-right-fill');
    });

    collapseButton.addEventListener('click', function() {
        if (navigationBar.classList.contains('collapsed')) {
            languageButton.style.display = 'none';
            accountname.style.display = 'none';
            dashboard.style.display = 'none';
            statistics.style.display = 'none';
            signout.style.display = 'none';

            collapseButton.style.margin = 'auto';
            collapseButton.style.display = 'block';
        } else {
            languageButton.style.display = 'block';
            accountname.style.display = 'block';
            dashboard.style.display = 'block';
            statistics.style.display = 'block';
            signout.style.display = 'block';
            mainContent.style.paddingRight = '0'; // terug naar origineel

            collapseButton.style.margin = 'unset';
        }
    });
}

//select subthemas