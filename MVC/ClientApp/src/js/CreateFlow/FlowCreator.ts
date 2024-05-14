import {Flow} from "../Flow/FlowObjects";
import {initializeDeleteButtons} from "./DeleteFlowModal";

const btnCreateFlow = document.getElementById("btnCreateFlow") as HTMLButtonElement;

export async function GetFlows(projectId: number) {
    console.log("Fetching flows...")
    await fetch("CreateFlow/GetFlows", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => UpdateFlowList(Object.values(data)))
        .then(() => initializeDeleteButtons())
        .catch(error => console.error("Error:", error))
    
}

export function UpdateFlowList(flows: Flow[]) {
    
    const flowContainer = document.getElementById("flow-cards") as HTMLElement;
    flowContainer.innerHTML = "";
    
    if (flows.length > 0) {
        flows.forEach(flow => {
            //Card container
            const flowCard = document.createElement('div');
            flowCard.classList.add("flow-card");
            flowCard.dataset.flowId = flow.id.toString();
            const flowButton = document.createElement('a');
            flowButton.classList.add("btn","flow-card-btn");
            flowButton.dataset.flowId = flow.id.toString();
            //Card Delete Button
            const flowCardDeleteBtn = document.createElement('button');
            flowCardDeleteBtn.innerHTML = '<i class="bi bi-trash3-fill"></i>';
            flowCardDeleteBtn.classList.add("btn", "btn-secondary", "flow-card-delete-btn");
            //Card View Button
            const flowCardViewBtn = document.createElement('a');
            flowCardViewBtn.innerHTML = '<i class="bi bi-eye-fill"></i>';
            flowCardViewBtn.classList.add("btn", "btn-secondary", "flow-card-view-btn")
            //Card Header
            const cardHeader = document.createElement('h2');
            cardHeader.classList.add("flow-card-header");
            cardHeader.innerText = "Flow " + flow.id.toString();
            //Card Footer
            const cardFooter = document.createElement('h3');
            cardFooter.classList.add("flow-card-footer");
            cardFooter.innerText = flow.flowType.toString();

            console.log("Adding flowCardViewBtn for flow: " + flow.id);
            flowCard.appendChild(flowCardViewBtn);
            flowCard.appendChild(flowCardDeleteBtn);
            flowButton.appendChild(cardHeader);
            flowButton.appendChild(cardFooter);
            flowCard.appendChild(flowButton);

            flowContainer.appendChild(flowCard);
        });
    } else {
        const noFlowsMessage = document.createElement('p');
        noFlowsMessage.classList.add('no-cards-message');
        noFlowsMessage.textContent = 'There are currently no Flows in this project!';
        flowContainer.appendChild(noFlowsMessage);
    }
    
    initializeCardLinks();
    
}

btnCreateFlow.onclick = async() => {

    let flowType = "Linear";
    
    try {
        const response = await fetch("CreateFlow/CreateFlow/" + flowType, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            }
        });
        if (response.ok) {
            console.log("Flow made successfully.");
        } else {
            console.error("Failed to make new flow.");
        }
    } catch (error) {
        console.error("Error:", error);
    }
    
    GetFlows(0);
}

function initializeCardLinks() {
    let flowCards = document.querySelectorAll('.flow-card-btn') as NodeListOf<HTMLAnchorElement>;

    flowCards.forEach(flowCard => {
        flowCard.addEventListener('click', () => {
            // Extract flow ID from flowCard dataset
            const flowId = flowCard.dataset.flowId;

            if (flowId) {
                const baseUrl = '/EditFlow/FlowEditor/';
                const url = `${baseUrl}${flowId}`;
                flowCard.setAttribute('href', url);
            } else {
                console.error('Flow ID not found in dataset');
            }
        });
    });
    
    initializeViewLinks();
}

function initializeViewLinks() {
    let flowViewButtons = document.querySelectorAll('.flow-card-view-btn') as NodeListOf<HTMLAnchorElement>;

    flowViewButtons.forEach(viewButton => {
        viewButton.addEventListener('click', () => {
            // Extract flow ID from viewButton dataset
            let flowCard = viewButton.parentNode as HTMLDivElement;
            const flowId = flowCard.dataset.flowId;

            if (flowId) {
                const baseUrl = '/Flow/Step/';
                const url = `${baseUrl}${flowId}`;
                viewButton.setAttribute('href', url);
            } else {
                console.error('Flow ID not found in dataset');
            }
        });
    });
}

document.addEventListener('DOMContentLoaded', () => {
    
    GetFlows(0).then(response => { initializeCardLinks(); }
        
    );
});
