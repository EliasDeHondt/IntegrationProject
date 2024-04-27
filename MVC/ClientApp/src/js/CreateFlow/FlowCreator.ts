import {Flow} from "../Flow/FlowObjects";

const btnCreateFlow = document.getElementById("btnCreateFlow") as HTMLButtonElement;

function GetFlows(projectId: number) {
    console.log("Fetching flows...")
    fetch("CreateFlow/GetFlows", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => UpdateFlowList(Object.values(data)))
        .catch(error => console.error("Error:", error))
    
}

async function UpdateFlowList(flows: Flow[]) {
    
    const flowContainer = document.getElementById("flow-cards") as HTMLElement;
    flowContainer.innerHTML = "";
    
    if (flows.length > 0) {
        flows.forEach(flow => {
            //Card Container
            const flowCard = document.createElement('div');
            flowCard.classList.add("flow-card");
            flowCard.dataset.flowId = flow.id.toString();
            //Card Delete Button
            const flowCardDeleteBtn = document.createElement('button');
            flowCardDeleteBtn.innerHTML = '<i class="bi bi-trash3-fill"></i>';
            flowCardDeleteBtn.classList.add("btn", "btn-secondary", "flow-card-delete-btn");
            //Card Header
            const cardHeader = document.createElement('h2');
            cardHeader.classList.add("flow-card-header");
            cardHeader.innerText = "Flow " + flow.id.toString();
            //Card Footer
            const cardFooter = document.createElement('h3');
            cardFooter.classList.add("flow-card-footer");
            cardFooter.innerText = flow.flowType.toString();

            flowCard.appendChild(flowCardDeleteBtn);
            flowCard.appendChild(cardHeader);
            flowCard.appendChild(cardFooter);

            flowContainer.appendChild(flowCard);
        });
    } else {
        const noFlowsMessage = document.createElement('p');
        noFlowsMessage.classList.add('no-cards-message');
        noFlowsMessage.textContent = 'There are currently no Flows in this project!';
        flowContainer.appendChild(noFlowsMessage);
    }
    
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

document.addEventListener('DOMContentLoaded', () => {
    GetFlows(0);
});
