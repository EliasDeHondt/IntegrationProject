import * as signalR from "@microsoft/signalr";
import {GenerateCards, GetFlowById} from "./FlowAPI";
import {Flow} from "../Flow/FlowObjects"

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

const divFlows = document.getElementById("flowContainer") as HTMLDivElement;

export let code = "";
let flows : Flow[] = [];

const storedCode = sessionStorage.getItem("connectionCode");
if (storedCode) {
    code = storedCode;
} else {
    code = Math.floor(100000 + Math.random() * 900000).toString();
    sessionStorage.setItem("connectionCode", code);
}

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
    for (let i = 0; i < ids.length; i++) {
       await GetFlowById(ids[i]).then(flow => flows[i] = flow)
    }
    GenerateCards(flows, divFlows);
})