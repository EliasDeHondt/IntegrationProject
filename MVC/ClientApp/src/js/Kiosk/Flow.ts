import * as kiosk from "./Kiosk";
import * as signalR from "@microsoft/signalr";
import {HubConnectionState} from "@microsoft/signalr";
import {Modal} from "bootstrap";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

const currFlow = document.getElementById("flowId") as HTMLSpanElement;
const modal = new Modal(document.getElementById("pausedFlowModal") as HTMLDivElement, {
    backdrop: 'static',
    keyboard: false
});
const btnExitFlow = document.getElementById("btnExitFlow") as HTMLButtonElement;

let currStateOfFlow = "";

document.addEventListener("DOMContentLoaded", async () => {
    await connection.start().then(() => {
        connection.invoke("JoinConnection", kiosk.code).then(() => {
            connection.invoke("ActivateFlow", kiosk.code, currFlow.innerText)
                .then(() => {
                    btnExitFlow.onclick = async () => {
                        window.location.href = `/Kiosk`
                    }
                })
        })
    });
})

connection.on("ReceiveFlowUpdate", async (id, state) => {
    currStateOfFlow = state;
    if (currStateOfFlow.toLowerCase() == "paused") {
        modal.show()
    } else {
        modal.hide()
    }
})

connection.on("FlowActivated", (id) => {
    window.location.href = `/Flow/Step/${id}`
})