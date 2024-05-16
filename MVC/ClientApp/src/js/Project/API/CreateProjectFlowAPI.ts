import {Flow} from "../../Flow/FlowObjects";

export function showFlows(flows: Flow[],forWhat: string,flowContainer: HTMLDivElement) {
    flowContainer.innerHTML = "";
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
            //Card View Button
            const flowCardViewBtn = document.createElement('a');
            flowCardViewBtn.innerHTML = '<i class="bi bi-eye-fill"></i>';
            flowCardViewBtn.classList.add("btn", "btn-secondary", "flow-card-view-btn");
            flowCardViewBtn.setAttribute('href', "/Flow/Step/" + flow.id);
            //Card Header
            const cardHeader = document.createElement('h2');
            cardHeader.classList.add("flow-card-header");
            cardHeader.innerText = "Flow " + flow.id.toString();
            //Card Footer
            const cardFooter = document.createElement('h3');
            cardFooter.classList.add("flow-card-footer");
            cardFooter.innerText = flow.flowType.toString();

            flowCard.appendChild(flowCardViewBtn);
            flowCard.appendChild(flowCardDeleteBtn);
            flowButton.appendChild(cardHeader);
            flowButton.appendChild(cardFooter);
            flowCard.appendChild(flowButton);

            flowContainer.appendChild(flowCard);
        });
    }
    initializeCardLinks();
    return flowContainer;
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