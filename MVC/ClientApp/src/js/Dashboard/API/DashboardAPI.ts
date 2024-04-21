import {User} from "../Types/UserTypes";


export async function getUsersForPlatform(platformId: string): Promise<User[]>{
    return await fetch("/api/Users/GetUsersForPlatform/" + platformId)
        .then(response => response.json())
        .then(data => {
            return data
        })
}

export function generateCard(user: User): HTMLDivElement {
    
    let colDiv = document.createElement("div");
    colDiv.className = "col mt-3 mb-3";
    let cardDiv = document.createElement("div");
    cardDiv.className = "card border-black border-2 bgAccent h-100";
    let cardBodyDiv = document.createElement("div");
    cardBodyDiv.className = "card-body";
    let pName = document.createElement("p");
    pName.className = "text-white";
    pName.textContent = "Name: " + user.userName;
    let pEmail = document.createElement("p");
    pEmail.className = "text-white";
    pEmail.textContent = "Email: " + user.email;
    
    cardBodyDiv.append(pName, pEmail);
    cardDiv.append(cardBodyDiv);
    colDiv.append(cardDiv);
    return colDiv;
}