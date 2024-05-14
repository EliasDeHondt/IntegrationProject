import {Flow} from "../Flow/FlowObjects";

function generateCard(flow: Flow): HTMLDivElement {
    const card = document.createElement("div");

    const enterButton = document.createElement("button");
    enterButton.id = "btnFlow";
    enterButton.innerText = "Flow " + flow.id.toString();
    enterButton.classList.add("kiosk-button-flow-control")
    enterButton.addEventListener("click", () => {
        window.location.href = `/Flow/Step/${flow.id}`;
    });

    card.appendChild(enterButton);
    return card;
}

export function GenerateCards(flows: Flow[], flowContainer: HTMLDivElement) {
    flowContainer.innerHTML = "";
    const cards = flows.map(generateCard);
    cards.forEach(card => {
        flowContainer.appendChild(card);
    })
}

export async function GetFlowsForProject(projectId: number): Promise<Flow[]> {
    return await fetch(`/api/Flows/GetFlowsForProject/${projectId}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    }).then(response => response.json())
        .then(data => {
            return data;
        })
}

export async function GetTypeOfFlows(type: string): Promise<Flow[]> {
    return await fetch(`/api/Flows/${type}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    }).then(response => response.json())
        .then(data => {
            return data;
        })
}

export async function GetFlowById(id: string): Promise<Flow> {
    return await fetch(`/api/Flows/${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    }).then(response => response.json())
        .then(data => {
            return data;
        })
}