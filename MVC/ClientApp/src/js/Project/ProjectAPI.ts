import {Project, SharedPlatform} from "./ProjectObjects";
import {Modal} from "bootstrap";
import {MainTheme} from "../Theme/ThemeObjects";
import {Flow} from "../Flow/FlowObjects";
import {generateProjectCard, getProjectsForPlatform} from "../Dashboard/API/DashboardAPI";
import {showUserName} from "../Layout/API/NavigationAPI";

let inputTitle = (document.getElementById("inputTitle") as HTMLInputElement);
let inputText = (document.getElementById("inputText") as HTMLInputElement);
const btnPublishProject = document.getElementById("btnPublishProject") as HTMLButtonElement;

// document.addEventListener('DOMContentLoaded', function() {
//     let id: string = document.getElementById("platformId")!.textContent!
//     const accountname = document.getElementById("accountname") as HTMLElement;
//
//     showUserName(id,accountname);
//     setupsaNavigation();
// });

function setupsaNavigation() {
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

function fillExisting(project: Project): void{
    inputTitle.value = project.title
    inputText.value = project.description
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
function getIdProject():number{
    let href = window.location.href;
    let regex = href.match(/\/Project\/Projects\/(\d+)/);

    if (regex) {
        const projectId = parseInt(regex[1], 10);
        return projectId;
    } else {
        console.error("Project ID not found in URL:", href);
        return 0;
    }
}
//Button
document.addEventListener("DOMContentLoaded", async function () {
    //let projectId = document.getElementById('btnPublishProject').getAttribute('data-project-id');
    const projectIdNumber = getIdProject();
    const project = await getProjectWithId(projectIdNumber);
    fillExisting(project);

    btnPublishProject.onclick = function () {
        console.log("click");

        if (CheckNotEmpty(inputTitle, "Title", "errorMsgTitle")) {
            SetProject(inputTitle.value, 2);

            window.location.href = "/SharedPlatform/Dashboard/";
        }
    };
    
    //todo sprint 3: maak api functie die deze ophaalt:
    // let id: string = "2" //document.getElementById("platformId")!.textContent!
    const accountname = document.getElementById("accountname") as HTMLElement;
    //
    // showUserName(id,accountname);
    accountname.textContent = "Welcome " + "Henk" + "!";
    setupsaNavigation();
});

//select subthemas todo Matthias