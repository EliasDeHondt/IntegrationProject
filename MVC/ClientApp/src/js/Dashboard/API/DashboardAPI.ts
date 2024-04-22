import {User} from "../Types/UserTypes";
import * as editModal from "../EditUserModal";


export async function getUsersForPlatform(platformId: string): Promise<User[]>{
    return await fetch("/api/Users/GetUsersForPlatform/" + platformId)
        .then(response => response.json())
        .then(data => {
            return data
        })
}

export function generateCard(user: User, userPermissions: boolean): HTMLDivElement {
    
    let colDiv = document.createElement("div");
    colDiv.className = "col mt-3 mb-3";
    let cardDiv = document.createElement("div");
    cardDiv.className = "card border-black border-2 bgAccent h-100";
    
    let utilsDiv;
    let editButton;
    let deleteButton;
    
    if(userPermissions) {
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
    if(userPermissions) {
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
    getUsersForPlatform(id).then(users => {
        users.forEach(user => {
            let card = generateCard(user, userPermissions);
            userRoulette.appendChild(card);
        })
    })
        .then(() => editModal.initializeEditButtons())
}