import {User} from "../Types/UserTypes";
import * as editModal from "../EditUserModal";
import {Project} from "../Types/ProjectObjects";
import * as deleteModal from "../DeleteUserModal";


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
        deleteButton.className = "border-0 p-0 h-50 w-100 deleteUser";
        deleteButton.style.background = "none";

        let deleteIcon = document.createElement("i");
        deleteIcon.className = "bi bi-person-fill-x modal-icon";
        deleteIcon.style.color = "white";

        editButton.append(editIcon);
        deleteButton.append(deleteIcon);
    }
    
    let cardBodyDiv = document.createElement("div");
    cardBodyDiv.className = "card-body";
    let iconDiv = document.createElement("div");
    iconDiv.className = "text-center";
    let icon = document.createElement("i");
    icon.className = "bi bi-person-fill";
    icon.style.color = "white";
    icon.style.fontSize = "10vh";
    let pName = document.createElement("p");
    pName.className = "text-white p-0";
    pName.style.marginTop = "-4vh"
    pName.style.marginBottom = "0"
    pName.textContent = user.userName;
    let pEmail = document.createElement("p");
    pEmail.className = "text-white visually-hidden";
    pEmail.textContent = "Email: " + user.email;
    
    iconDiv.append(icon, pName);
    cardBodyDiv.append(iconDiv,pEmail);
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
    let cardCreateUser = document.getElementById("createUserSlide") as HTMLDivElement;
    if(userPermissions) cardCreateUser.style.display = "block";
    else cardCreateUser.style.display = "none";
    getUsersForPlatform(id).then(users => {
        getLoggedInEmail().then(email => {
            users.forEach(user => {
                let card = generateUserCard(user, userPermissions, email);
                userRoulette.appendChild(card);
            })
        })
            .then(() => {
                editModal.initializeEditButtons()
                deleteModal.initializeDeleteButtons()
            })
    })
}

async function getLoggedInEmail(): Promise<string> {
    return await fetch("/api/Users/GetLoggedInEmail")
        .then(response => {
            return response.text()
        })
}

//Projects
export function resetProjectCards(id: string, projectRoulette: HTMLDivElement) {
    let length = projectRoulette.children.length
    for (let i = length - 1; i > 0; i--){
        projectRoulette.children[i].remove();
    }
    generateProjectCards(id, projectRoulette);
}
export async function getProjectsForPlatform(platformId: string): Promise<Project[]>{
    return await fetch("/api/Projects/GetProjectsForPlatform/" + platformId)
        .then(response => response.json())
        .then(data => {
            return data
        })
}

export function generateProjectCard(project: Project): HTMLDivElement {
    let colDiv = document.createElement("div");
    colDiv.className = "col mt-3 mb-3 embla__slide";
    let cardDiv = document.createElement("div");
    cardDiv.className = "card border-black border-2 bgAccent h-100";
    cardDiv.style.height = "250px";
    cardDiv.style.position = "relative";

    // Buttons
    let btnHideProject = createButton("btnHideProject", "bi-eye");
    let btnDeleteProject = createButton("btnDeleteProject", "bi-folder-minus");
    let btnGraphProject = createButton("btnGraphProject", "bi-graph-up");
    let btnEnterProject = createButton("btnEnterProject", "bi-folder");

    btnHideProject.className = "border-0 p-0 position-absolute top-0 end-1 ms-2 mb-2\" style=\"background: none;";
    btnDeleteProject.className = "border-0 p-0 position-absolute top-0 end-0 me-2 mb-2\" style=\"background: none;";
    btnGraphProject.className = "border-0 p-0 position-absolute top-0 end-0 mt-5 me-2 mb-2\" style=\"background: none;";
    btnEnterProject.className = "border-0 p-0";
    btnEnterProject.style.background = "none;";
    btnEnterProject.style.fontSize = "10vh";

    btnHideProject.style.fontSize = "3vh";
    btnDeleteProject.style.fontSize = "3vh";
    btnGraphProject.style.fontSize = "3vh";
    

    let cardBodyDiv = document.createElement("div");
    cardBodyDiv.className = "card-body align-items-center d-flex justify-content-center";

    // Edit Project Link
    let editProjectLink = document.createElement("a");
    let projectId = project.id;
    console.log(project.id, project.name, project.description)
    editProjectLink.className = "nav-link text-light";
    editProjectLink.setAttribute("href", "/Project/Projects/" + projectId);
    editProjectLink.textContent = project.name;

    let projDiv = document.createElement("div");
    projDiv.className = "text-center";
    btnEnterProject.setAttribute("href", "/Project/Projects/" + projectId);
    projDiv.append(btnEnterProject, editProjectLink);
    
    
    // let a = document.createElement("a");
    // a.textContent = project.name + ": "
    //cardBodyDiv.appendChild(a)
    
    cardBodyDiv.appendChild(projDiv);
    //cardBodyDiv.appendChild(btnEnterProject);
    
    
    cardDiv.appendChild(btnHideProject);
    cardDiv.appendChild(btnDeleteProject);
    cardDiv.appendChild(btnGraphProject);
    cardDiv.appendChild(cardBodyDiv);

    colDiv.appendChild(cardDiv);

    btnHideProject.addEventListener("click", function() {
        // @ts-ignore
        btnHideProject.firstChild.classList.toggle("bi-eye-slash");
        // @ts-ignore
        btnHideProject.firstChild.classList.toggle("bi-eye");
    });
    
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
export function generateProjectCards(id: string, projectRoulette: HTMLDivElement) {
    let cardCreateProject = document.getElementById("cardCreateProject") as HTMLDivElement;
    cardCreateProject.style.display = "block";
    id = '2';
    getProjectsForPlatform(id).then(projects => {
        projects.forEach(project => {
            let card = generateProjectCard(project);
            projectRoulette.appendChild(card);
        })})
}