import * as signalR from "@microsoft/signalr";
import "./ChooseFlow";
import "./FlowTypeModal";
import {flowTypeModal} from "./FlowTypeModal";
import {GetFlows} from "../Kiosk/FlowAPI";
import {GenerateOptions} from "./ChooseFlow";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

const btnPauseFlow = document.getElementById("btnPauseFlow") as HTMLButtonElement;
const cardCurrentFlow = document.getElementById("cardCurrentFlow") as HTMLDivElement; // Card moet nog aangemaakt worden in typescript ipv HTML
const btnCloseInstallation = document.getElementById("cardCurrentFlow") as HTMLButtonElement;
const connectionCode = document.getElementById("connectionCode") as HTMLSpanElement;

let currentFlow = document.getElementById("currentFlow") as HTMLHeadingElement;
let currentState = document.getElementById("currentState") as HTMLSpanElement;
let code: string = "";

document.addEventListener("DOMContentLoaded", async () => {
    currentFlow.innerText = "0"
    currentState.innerText = "Inactive"
    const btnEnterCode = document.getElementById("btnEnterCode") as HTMLButtonElement;
    await connection.start()
        .then(() => {
            btnEnterCode.onclick = async () => {
                code = (document.getElementById("inputCode") as HTMLInputElement).value;
                await connection.invoke("JoinConnection", code).then(() => connectionCode.innerText = code)
                flowTypeModal.show();
            };
        })
})

connection.on("FlowActivated", (id) => {
    currentFlow.innerText = `${id}`
    currentState.innerText = "Active"
})

connection.on("ReceiveFlowUpdate", (id, state) => {
    currentFlow.innerText = `${id}`
    currentState.innerText = `${state}`
})

function SendFlowUpdate() {
    connection.invoke("SendFlowUpdate", code, currentFlow.innerText, currentState.innerText)
        .then(() => console.log(code, currentFlow.innerText, currentState.innerText))
        .catch(error => console.error(error))
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