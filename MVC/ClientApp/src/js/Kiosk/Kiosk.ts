import * as signalR from "@microsoft/signalr";
import {GenerateCards, GetFlowById} from "./FlowAPI";
import {Flow} from "../Flow/FlowObjects"
import {getFlowType} from "../Flow/ChooseFlow";

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
    const connectionCode = document.getElementById("connectionCode") as HTMLParagraphElement;
    connectionCode.innerText = code;

    await connection.start().then(() => {
        connection.invoke("JoinConnection", code).then(() => {
            connection.invoke("SendFlowUpdate", code, "0", "Inactive");
        })
    })
})

connection.on("ReceiveSelectedFlowIds", async (ids, flowType) => {
    if(flowType != "physical"){
        await GenerateFlowOptions(ids);
    } else {
        window.location.href = `/Flow/Step/${ids[0]}`
    }
    sessionStorage.setItem("flowType", flowType);
    sessionStorage.setItem("flowOptions", ids);
    storedFlows = sessionStorage.getItem("flowOptions")
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
connection.on("UserJoinedConnection", () => {
    const projectId = Number.parseInt(document.getElementById("projectId")!.dataset.projectId!);
    connection.invoke("SendProjectId", code, projectId)  
    if(storedFlows != null){
        connection.invoke("OngoingFlow", code, true)
    } else {
        connection.invoke("OngoingFlow",code, false)
    }
})

window.onclose = () => {
    connection.invoke("LeaveConnection", code, code);
}

connection.on("FlowActivated", (id) => {
    window.location.href = `/Flow/Step/${id}`
})