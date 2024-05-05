import * as signalR from "@microsoft/signalr";
import "./ChooseFlow";
import "./FlowTypeModal";
import {flowTypeModal} from "./FlowTypeModal";
import {GetFlows} from "../Kiosk/FlowAPI";
import {GenerateOptions, SubmitFlows} from "./ChooseFlow";

export const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

const btnPauseFlow = document.getElementById("btnPauseFlow") as HTMLButtonElement;
const cardCurrentFlow = document.getElementById("cardCurrentFlow") as HTMLDivElement; // Card moet nog aangemaakt worden in typescript ipv HTML
const btnCloseInstallation = document.getElementById("cardCurrentFlow") as HTMLButtonElement;
const connectionCode = document.getElementById("connectionCode") as HTMLSpanElement;
const btnSubmitFlows = document.getElementById("btnSubmitFlows") as HTMLButtonElement;

let currentFlow = document.getElementById("currentFlow") as HTMLHeadingElement;
let currentState = document.getElementById("currentState") as HTMLSpanElement;
export let code = "";


document.addEventListener("DOMContentLoaded", async () => {const storedCode = sessionStorage.getItem("connectionCode");
    if (storedCode) {
        code = storedCode;
    } else {
        code = Math.floor(100000 + Math.random() * 900000).toString();
        sessionStorage.setItem("connectionCode", code);
    }

    currentFlow.innerText = "0"
    currentState.innerText = "Inactive"
    const btnEnterCode = document.getElementById("btnEnterCode") as HTMLButtonElement;
    await connection.start()
        .then(() => {
            btnEnterCode.onclick = async () => {
                code = (document.getElementById("inputCode") as HTMLInputElement).value;
                await connection.invoke("JoinConnection", code).then(() => {
                    connectionCode.innerText = code
                    console.log("connection #" + code);
                })
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