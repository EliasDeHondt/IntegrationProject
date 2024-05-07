import * as signalR from "@microsoft/signalr";
import "./ChooseFlow";
import {flowTypeModal} from "./FlowTypeModal";
import {GetFlows} from "../Kiosk/FlowAPI";
import {Flow} from "./FlowObjects"

export const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

const btnPauseFlow = document.getElementById("btnPauseFlow") as HTMLButtonElement;
const btnCloseInstallation = document.getElementById("btnCloseInstallation") as HTMLButtonElement;
const connectionCode = document.getElementById("connectionCode") as HTMLSpanElement;
const flowContainer = document.getElementById("carouselContainer") as HTMLDivElement;

let currentFlow = document.getElementById("currentFlow") as HTMLHeadingElement;
let currentState = document.getElementById("currentState") as HTMLSpanElement;
export let code = "";


document.addEventListener("DOMContentLoaded", async () => {
    GetFlows().then(flows => GenerateFlowCards(flows, flowContainer));
    
    const storedCode = sessionStorage.getItem("connectionCode");
    if (storedCode) {
        code = storedCode;
    } else {
        code = Math.floor(100000 + Math.random() * 900000).toString();
        sessionStorage.setItem("connectionCode", code);
    }

    const btnEnterCode = document.getElementById("btnEnterCode") as HTMLButtonElement;
    await connection.start()
        .then(() => {
            btnEnterCode.onclick = async () => {
                code = (document.getElementById("inputCode") as HTMLInputElement).value;
                await connection.invoke("JoinConnection", code).then(() => {
                    connectionCode.innerText = code
                })
                flowTypeModal.show();
            };
        })

    currentFlow.innerText = "0"
    currentState.innerText = "Inactive"
    if (btnCloseInstallation)
        btnCloseInstallation.onclick = () => {
            connection.invoke("LeaveConnection", "Facilitator", code).then(() => ConnectionClosed())
        }

})

connection.on("FlowActivated", (id) => {
    currentFlow.innerText = `${id}`
    currentState.innerText = "Active"
})

connection.on("ReceiveFlowUpdate", (id, state) => {
    currentFlow.innerText = `${id}`
    currentState.innerText = `${state}`
})

connection.on("UserLeftConnection", (message) => {
    console.log(message)
    connection.invoke("LeaveConnection", "Facilitator", code).then(() => ConnectionClosed());
});

function SendFlowUpdate() {
    connection.invoke("SendFlowUpdate", code, currentFlow.innerText, currentState.innerText)
}

btnPauseFlow.onclick = () => {
    if (currentState.innerText == "Active") {
        currentState.innerText = "Paused";
        btnPauseFlow.innerText = "Unpause flow";
    } else {
        currentState.innerText = "Active";
        btnPauseFlow.innerText = "Pause flow";
    }
    SendFlowUpdate()
}

function ConnectionClosed() {
    code = "";
    connectionCode.innerText = "";
    currentFlow.innerText = "0"
    currentState.innerText = "Inactive"
}

function ActivateFlow(flowId: string) {
    connection.invoke("ActivateFlow", code, flowId).then(() => console.log(`Activate flow ${flowId}`));
}

// Facilitator dashboard

function GenerateFlowCard(flow: Flow): HTMLDivElement {
    const cardContainer = document.createElement("div");
    cardContainer.classList.add("col", "mt-3", "mb-3", "embla__slide");

    const card = document.createElement("div");
    card.classList.add("card", "border-black", "border-2", "bgAccent", "h-100");
    card.style.height = "150px";

    const cardBody = document.createElement("div");
    cardBody.classList.add("card-body", "align-items-center", "d-flex", "justify-content-center");


    const enterButton = document.createElement("button");
    enterButton.classList.add("border-0", "p-0");
    enterButton.style.background = "none";
    enterButton.innerHTML = `<i class="bi bi-folder" style="color: white; font-size: 10vh;"></i>`;
    enterButton.addEventListener("click", () => {
        ActivateFlow(flow.id.toString());
    });

    const title = document.createElement("p")
    title.className = "text-center text-white";
    title.style.marginTop = "-4vh"
    title.style.marginBottom = "0"
    title.textContent = flow.id.toString();

    enterButton.appendChild(title);
    cardBody.appendChild(enterButton);
    card.appendChild(cardBody);
    cardContainer.appendChild(card)
    return cardContainer;
}

function GenerateFlowCards(flows: Flow[], flowContainer: HTMLDivElement) {
    const cards = flows.map(GenerateFlowCard);
    cards.forEach(card => {
        flowContainer.appendChild(card);
    })
}