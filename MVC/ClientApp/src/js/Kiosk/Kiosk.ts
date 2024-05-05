import * as signalR from "@microsoft/signalr";
import {GenerateCards, GetFlowById} from "./FlowAPI";
import {Flow} from "../Flow/FlowObjects"
import {div} from "@tensorflow/tfjs";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

const divFlows = document.getElementById("flowContainer") as HTMLDivElement;

export let code = "";
let selectedFlowIds: string[] = [];
let flows: Flow[] = [];

const storedCode = sessionStorage.getItem("connectionCode");
if (storedCode) {
    code = storedCode;
} else {
    code = Math.floor(100000 + Math.random() * 900000).toString();
    sessionStorage.setItem("connectionCode", code);
}

let storedFlows = sessionStorage.getItem("flowOptions");

document.addEventListener("DOMContentLoaded", async () => {
    const connectionCode = document.getElementById("connectionCode") as HTMLSpanElement;
    connectionCode.innerText = code;

    await connection.start().then(() => {
        connection.invoke("JoinConnection", code)
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
    for (let i = 0; i < ids.length; i++) {
        await GetFlowById(ids[i]).then(flow => flows[i] = flow)
    }
    GenerateCards(flows, divFlows);
}