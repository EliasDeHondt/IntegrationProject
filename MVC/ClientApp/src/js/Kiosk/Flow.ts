import {code} from "./Kiosk";
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
        connection.invoke("JoinConnection", code).then(() => {
            connection.invoke("ActivateFlow", code, currFlow.innerText)
                .then(() => {
                    btnExitFlow.onclick = async () => {
                        await connection.invoke("SendFlowUpdate", code, "0", "Inactive").then(() =>
                            console.log("connection #" + code));
                        window.location.href = `/Kiosk`
                    }
                })
        })
    });
})

/*

export async function UpdateFlowState(id: string, state: string) {
    fetch("/api/Flows/" + id + "/" + state, {
        method: "PUT"
    })
        .then(response => {
            if (response.ok) {
                console.log(`Flow ${state}!`)
                return true;
            }
            return false;
        })
        .catch(error => console.error("Error:", error))
}

async function UpdateCurrentFlowState() {
    if (prevFlowId != null)
        await UpdateFlowState(prevFlowId, 'Inactive');
    await UpdateFlowState(String(flowId), 'Active');
    sessionStorage.setItem('prevFlowId', String(flowId));
}*/


connection.on("ReceiveFlowUpdate", async (id, state) => {
    console.log(id, state)
    currStateOfFlow = state;
    if (currStateOfFlow.toLowerCase() == "paused") {
        modal.show()
    } else {
        modal.hide()
    }
})
