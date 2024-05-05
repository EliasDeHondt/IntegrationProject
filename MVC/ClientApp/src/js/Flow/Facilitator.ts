import * as signalR from "@microsoft/signalr";
import "./ChooseFlow";
import "./FlowTypeModal";
import {flowTypeModal} from "./FlowTypeModal";

export const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

const btnPauseFlow = document.getElementById("btnPauseFlow") as HTMLButtonElement;
const btnCloseInstallation = document.getElementById("btnCloseInstallation") as HTMLButtonElement;
const connectionCode = document.getElementById("connectionCode") as HTMLSpanElement;

let currentFlow = document.getElementById("currentFlow") as HTMLHeadingElement;
let currentState = document.getElementById("currentState") as HTMLSpanElement;
export let code = "";


document.addEventListener("DOMContentLoaded", async () => {
    const storedCode = sessionStorage.getItem("connectionCode");
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
                })
                flowTypeModal.show();
            };
        })

    if (btnCloseInstallation)
        btnCloseInstallation.onclick = () => {
            connection.invoke("LeaveConnection", code).then(() => {
                code = "";
                connectionCode.innerText = "";
                currentFlow.innerText = "0"
                currentState.innerText = "Inactive"
            })
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