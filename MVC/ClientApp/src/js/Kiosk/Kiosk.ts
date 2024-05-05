import * as signalR from "@microsoft/signalr";
import {GenerateCards, GetFlowById} from "./FlowAPI";
import {Flow} from "../Flow/FlowObjects"
import {div} from "@tensorflow/tfjs";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

const divFlows = document.getElementById("flowContainer") as HTMLDivElement;

export let code = "";
let selectedFlowIds : string[] = [];
let flows : Flow[] = [];

const storedCode = sessionStorage.getItem("connectionCode");
if (storedCode) {
    code = storedCode;
} else {
    code = Math.floor(100000 + Math.random() * 900000).toString();
    sessionStorage.setItem("connectionCode", code);
}

let storedFlows = sessionStorage.getItem("selectedFlows");

document.addEventListener("DOMContentLoaded", async () => {
    const connectionCode = document.getElementById("connectionCode") as HTMLSpanElement;
    connectionCode.innerText = code;
    
    await connection.start().then(() => {
        connection.invoke("JoinConnection", code).then(() =>
            console.log("connection #" + code))
    });

    GenerateCards(flows, divFlows);
})

connection.on("ReceiveSelectedFlowIds", async (ids) => {
    if (storedFlows) {
        storedFlows = storedFlows.split(",").join("")
        for (let i = 0; i < storedFlows.length; i++) {
            await GetFlowById(storedFlows[i]).then(flow => flows[i] = flow)
        }
    } else {
        for (let i = 0; i < ids.length; i++) {
            await GetFlowById(ids[i]).then(flow => flows[i] = flow)
        }
        sessionStorage.setItem("selectedFlows", ids);
    }
    GenerateCards(flows, divFlows);
})