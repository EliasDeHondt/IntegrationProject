import {FacilitatorUpdate, User} from "../../Types/UserTypes";
import {Project} from "../../Types/ProjectObjects";

export async function getUser(email: string): Promise<User>{
    return await fetch("/api/Users/GetUser/" + email, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(response => response.json()).then(data => {
        return data;
    })
}

export async function updateUser(email: string, user: User){
    await fetch("/api/Users/UpdateUser/" + email, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(user)
    })
}

export async function updateFacilitator(email: string, user: FacilitatorUpdate) {
    await fetch("/api/Users/UpdateFacilitator/" + email, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(user)
    })
}

export async function getPossibleProjects(email: string): Promise<Project[]>{
    return await fetch("/api/Users/GetPossibleProjectsForFacilitator/" + email, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(response => response.json()).then(data => {
      return data;  
    })
}

export async function getAssignedProjects(email: string): Promise<Project[]>{
    return await fetch("/api/Users/GetAssignedProjectsForFacilitator/" + email, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(response => response.json())
        .then(data => { return data; })
}