import {Flow} from "../../../Flow/FlowObjects";

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

export function showFlows(flows: Flow[], flowContainer: HTMLTableSectionElement) {
    flowContainer.innerHTML = "";
    flows.forEach(flow => addFlow(flow, flowContainer));
}

function addFlow(flow: Flow, flowContainer: HTMLTableSectionElement ) {
    flowContainer.innerHTML += `<tr>
                                   <td>${flow.id}</td>
                                   <td>${flow.flowType.toString()}</td>
                                   <td><a>Edit Flow</a></td>
                                </tr>`
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