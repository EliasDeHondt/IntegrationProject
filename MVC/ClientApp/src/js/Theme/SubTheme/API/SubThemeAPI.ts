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
        // .then(response => response.json())
        // .then(data => UpdateFlowList(Object.values(data)))
        // .then(() => initializeDeleteButtons())
        .catch(error => console.error("Error:", error))
    
}

export function showFlows(flows: Flow[],forWhat: string) {
    const flowContainer = document.getElementById("flow-cards") as HTMLElement;
    //flowContainer.innerHTML = "";

    if (flows.length > 0) {
        flows.forEach(flow => {
            //Card container
            const flowCard = document.createElement('div');
            flowCard.classList.add("flow-card");
            flowCard.dataset.flowId = flow.id.toString();
            
            if(forWhat === "forProject"){
                flowCard.classList.add("forProject");
            }
            if(forWhat === "forSubtheme"){
                flowCard.classList.add("forSubtheme");
            }
            
            const flowButton = document.createElement('a');
            flowButton.classList.add("btn","flow-card-btn");
            flowButton.dataset.flowId = flow.id.toString();
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
}
// function addFlow(flow: Flow, flowContainer: HTMLTableSectionElement ) {
//     flowContainer.innerHTML += `<tr>
//                                    <td>${flow.id}</td>
//                                    <td>${flow.flowType.toString()}</td>
//                                    <td><a>Edit Flow</a></td>
//                                 </tr>`
// }


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