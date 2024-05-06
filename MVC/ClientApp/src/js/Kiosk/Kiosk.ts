import * as signalR from "@microsoft/signalr";
import {GenerateCards, GetFlowById} from "./FlowAPI";
import {Flow} from "../Flow/FlowObjects"
import {div} from "@tensorflow/tfjs";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

const divFlows = document.getElementById("flowContainer") as HTMLDivElement;

export let code = "";

const storedCode = sessionStorage.getItem("connectionCode");
if (storedCode) {
    code = storedCode;
} else {
    code = Math.floor(100000 + Math.random() * 900000).toString();
    sessionStorage.setItem("connectionCode", code);
}

let storedFlows = sessionStorage.getItem("flowOptions");

document.addEventListener("DOMContentLoaded", async () => {
    console.log(code)
    const connectionCode = document.getElementById("connectionCode") as HTMLSpanElement;
    connectionCode.innerText = code;

    await connection.start().then(() => {
        connection.invoke("JoinConnection", code).then(() => {
            connection.invoke("SendFlowUpdate", code, "0", "Inactive");
        })
    })
})

connection.on("ReceiveSelectedFlowIds", async (ids) => {
    await GenerateFlowOptions(ids);
    sessionStorage.setItem("flowOptions", ids);
})

if (storedFlows) {
    storedFlows = storedFlows.split(",").join("");
    GenerateFlowOptions(storedFlows).then(r => console.log(r));
}

async function GenerateFlowOptions(ids: string) {
    let flows: Flow[] = [];
    for (let i = 0; i < ids.length; i++) {
        await GetFlowById(ids[i]).then(flow => flows[i] = flow)
    }
    GenerateCards(flows, divFlows);
}

connection.on("UserLeftConnection", (message) => console.log(message))

window.onclose = () => {
    connection.invoke("LeaveConnection", code, code);
}

connection.on("FlowActivated", (id) => {
    window.location.href = `/Flow/Step/${id}`
})