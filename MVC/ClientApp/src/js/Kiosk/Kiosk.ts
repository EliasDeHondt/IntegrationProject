import * as signalR from "@microsoft/signalr";
import {GenerateCards, GetFlows} from "./FlowAPI";

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

document.addEventListener("DOMContentLoaded", async () => {
    const connectionCode = document.getElementById("connectionCode") as HTMLSpanElement;
    connectionCode.innerText = code;
    
    await connection.start().then(() => {
        connection.invoke("JoinConnection", code)
    });
    
    GetFlows().then(flows => GenerateCards(flows, divFlows));
})