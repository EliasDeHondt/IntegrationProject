import {Flow} from "../Flow/FlowObjects";

function generateCard(flow: Flow): HTMLDivElement{
    const cardContainer = document.createElement("div");
    
    const card = document.createElement("div");

    const cardBody = document.createElement("div");

    const enterButton = document.createElement("button");
    enterButton.id = "btnFlow";
    enterButton.addEventListener("click", () => {
        window.location.href = `/Kiosk/Flow/${flow.id}`;
    });

    const title = document.createElement("p")
    title.textContent = flow.id.toString();

    enterButton.appendChild(title)
    cardBody.appendChild(enterButton);
    card.appendChild(cardBody);
    cardContainer.appendChild(card)
    return cardContainer;
}

export function GenerateCards(flows: Flow[], flowContainer: HTMLDivElement) {
    const cards = flows.map(generateCard);

    cards.forEach(card => {
        flowContainer.appendChild(card);
    })
}

export async function GetFlows(): Promise<Flow[]>{
    return await fetch("/api/Flows", {
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