import {User} from "../Types/UserTypes";
import * as editModal from "../EditUserModal";
import {Project} from "../Types/ProjectObjects";

//Users
export async function getUsersForPlatform(platformId: string): Promise<User[]>{
    return await fetch("/api/Users/GetUsersForPlatform/" + platformId)
        .then(response => response.json())
        .then(data => {
            return data
        })
}

export function generateUserCard(user: User, userPermissions: boolean, email:string): HTMLDivElement {
    
    let colDiv = document.createElement("div");
    colDiv.className = "col mt-3 mb-3 embla__slide";
    let cardDiv = document.createElement("div");
    cardDiv.className = "card border-black border-2 bgAccent h-100";
    
    let utilsDiv;
    let editButton;
    let deleteButton;
    
    if(userPermissions && user.email != email) {
        utilsDiv = document.createElement("div");
        utilsDiv.className = "position-absolute top-0 end-0 me-2 h-50";

        editButton = document.createElement("button");
        editButton.className = "border-0 p-0 h-50 w-100 editUser";
        editButton.style.background = "none";

        let editIcon = document.createElement("i");
        editIcon.className = "bi bi-person-fill-gear modal-icon";
        editIcon.style.color = "white";

        deleteButton = document.createElement("button");
        deleteButton.className = "border-0 p-0 h-50 w-100";
        deleteButton.style.background = "none";

        let deleteIcon = document.createElement("i");
        deleteIcon.className = "bi bi-person-fill-x modal-icon";
        deleteIcon.style.color = "white";

        editButton.append(editIcon);
        deleteButton.append(deleteIcon);
    }
    
    let cardBodyDiv = document.createElement("div");
    cardBodyDiv.className = "card-body";
    let pName = document.createElement("p");
    pName.className = "text-white";
    pName.textContent = "Name: " + user.userName;
    let pEmail = document.createElement("p");
    pEmail.className = "text-white";
    pEmail.textContent = "Email: " + user.email;
    
    cardBodyDiv.append(pName, pEmail);
    if(userPermissions && user.email != email) {
        utilsDiv!.append(editButton!, deleteButton!)
        cardDiv.append(utilsDiv!)
    }
    cardDiv.append(cardBodyDiv);
    colDiv.append(cardDiv);
    return colDiv;
}

export function resetCards(id: string, userRoulette: HTMLDivElement, userPermissions: boolean) {
    let length = userRoulette.children.length
    for (let i = length - 1; i > 0; i--){
        userRoulette.children[i].remove();
    }
    generateUserCards(id, userRoulette, userPermissions);
}

export function generateUserCards(id: string, userRoulette: HTMLDivElement, userPermissions: boolean) {
    let cardCreateUser = document.getElementById("cardCreateUser") as HTMLDivElement;
    if(userPermissions) cardCreateUser.style.display = "block";
    else cardCreateUser.style.display = "none";
    getUsersForPlatform(id).then(users => {
        getLoggedInEmail().then(email => {
            users.forEach(user => {
                let card = generateUserCard(user, userPermissions, email);
                userRoulette.appendChild(card);
            })
        })
            .then(() => editModal.initializeEditButtons())
    })
}

async function getLoggedInEmail(): Promise<string> {
    return await fetch("/api/Users/GetLoggedInEmail")
        .then(response => {
            return response.text()
        })
}

//Projects
export async function getProjectsForPlatform(platformId: string): Promise<Project[]>{
    return await fetch("/api/Projects/GetProjectsForPlatform/" + platformId)
        .then(response => response.json())
        .then(data => {
            return data
        })
}
export function generateProjectCard(project: Project): HTMLDivElement {
    let colDiv = document.createElement("div");
    colDiv.className = "col mt-3 mb-3";
    let cardDiv = document.createElement("div");
    cardDiv.className = "card border-black border-2 bgAccent h-100";
    cardDiv.style.height = "150px";

    // Buttons
    let btnHideProject = createButton("btnHideProject", "bi-eye");
    let btnDeleteProject = createButton("btnDeleteProject", "bi-folder-minus");
    let btnGraphProject = createButton("btnGraphProject", "bi-graph-up");
    let btnEnterProject = createButton("btnEnterProject", "bi-folder");

    btnHideProject.className = "border-0 p-0 position-absolute top-0 end-1 ms-2\" style=\"background: none;";
    btnDeleteProject.className = "border-0 p-0 position-absolute top-0 end-0 me-2\" style=\"background: none;";
    btnGraphProject.className = "border-0 p-0 position-absolute top-0 end-0 mt-5 me-2\" style=\"background: none;";
    btnEnterProject.className = "border-0 p-0";
    btnEnterProject.style.background = "none;";


    let cardBodyDiv = document.createElement("div");
    cardBodyDiv.className = "card-body align-items-center d-flex justify-content-center";

    // Edit Project Link
    let editProjectLink = document.createElement("a");
    editProjectLink.className = "nav-link text-light";
    editProjectLink.setAttribute("href", "/Project/Projects/1");
    editProjectLink.textContent = "Edit Project";

    cardBodyDiv.appendChild(editProjectLink);
    cardBodyDiv.appendChild(btnEnterProject);

    let a = document.createElement("a");
    a.textContent = project.title
    
    cardDiv.appendChild(btnHideProject);
    cardDiv.appendChild(btnDeleteProject);
    cardDiv.appendChild(btnGraphProject);
    cardDiv.appendChild(cardBodyDiv);

    colDiv.appendChild(cardDiv);

    return colDiv;
}

function createButton(id: string, iconClass: string): HTMLButtonElement {
    const button = document.createElement("button");
    button.id = id;
    button.className = "border-0 p-0 position-absolute top-0 end-0";
    button.style.background = "none";

    const icon = document.createElement("i");
    icon.className = `bi ${iconClass}`;
    icon.style.color = "white";

    button.appendChild(icon);

    return button;
}
export function generateProjectCards(id: string, userRoulette: HTMLDivElement) {
    let cardCreateProject = document.getElementById("cardCreateProject") as HTMLDivElement;
    cardCreateProject.style.display = "block";
    id = '2'
    getProjectsForPlatform(id).then(projects => {
        projects.forEach(project => {
            console.log(project.title)
            let card = generateProjectCard(project);
            userRoulette.appendChild(card);
        })})
        .then(() => editModal.initializeEditButtons())
}