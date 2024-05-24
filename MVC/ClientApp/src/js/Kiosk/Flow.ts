import * as kiosk from "./Kiosk";
import * as signalR from "@microsoft/signalr";
import {Modal} from "bootstrap";
import * as stepAPI from "../Flow/StepAPI";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

const currFlow = document.getElementById("flowId") as HTMLSpanElement;
const modal = new Modal(document.getElementById("pausedFlowModal") as HTMLDivElement, {
    backdrop: 'static',
    keyboard: false
});

let currStateOfFlow = "";

document.addEventListener("DOMContentLoaded", async () => {
    await connection.start().then(() => {
        connection.invoke("JoinConnection", kiosk.code).then(() => {
            connection.invoke("ActivateFlow", kiosk.code, currFlow.innerText)
        })
    });
})

connection.on("ReceiveFlowUpdate", async (id, state) => {
    currStateOfFlow = state;
    if (currStateOfFlow.toLowerCase() == "paused") {
        modal.show()
        stepAPI.stepTimer.pause();
        stepAPI.clockTimer.pause();
    } else {
        modal.hide()
        stepAPI.stepTimer.resume();
        stepAPI.clockTimer.resume();
    }
})

connection.on("FlowActivated", (id) => {
    window.location.href = `/Flow/Step/${id}`
})

connection.on("CurrentFlowRestarted", async () => {
    await stepAPI.restartFlow()
})