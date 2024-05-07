import {Flow} from "../../../Flow/FlowObjects";
import {initializeDeleteButtons} from "../../../CreateFlow/DeleteFlowModal";
import {GetFlows} from "../../../CreateFlow/FlowCreator";

export async function loadFlows(id: number): Promise<Flow[]> {
    return await fetch(`/api/SubThemes/${id}/Flows`, {
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

export async function updateSubTheme(id: number, subject: string) {
    await fetch(`/api/SubThemes/UpdateSubTheme/${id}`, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            subject: subject
        })
    })
}