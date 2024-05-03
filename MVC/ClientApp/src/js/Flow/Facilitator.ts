import * as signalR from "@microsoft/signalr";

const btnPauseFlow = document.getElementById("btnPauseFlow") as HTMLButtonElement;

let currentFlow = document.getElementById("currentFlow") as HTMLHeadingElement;
let currentState = document.getElementById("currentState") as HTMLSpanElement;
let currentCode: string = "";

export const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

export const connectionStart = async () => {
    await connection.start()
        .then(() => {
            console.log("SignalR connection started successfully.");
        })
}

document.addEventListener("DOMContentLoaded", async function () {
    const btnEnterCode = document.getElementById("btnEnterCode") as HTMLInputElement;
    await connectionStart();
    if (btnEnterCode)
        btnEnterCode.onclick = () => {
            currentCode = (document.getElementById("inputCode") as HTMLInputElement).value;
            connection.invoke("ConnectToUser", currentCode)
                .then(() => {
                    console.log("Connected to installation #" + currentCode);
                })
                .catch((err) => {
                    console.error("Error user connection: " + err + currentCode);
                });
        };
});

connection.on("ReceiveFlowUpdate", (id, state) => {
    console.log(id, state)
    currentFlow.innerText = `${id}`
    currentState.innerText = `${state}`
})

function SendUpdate() {
    connection.invoke("SendFlowUpdate", currentCode, currentFlow.innerText, currentState.innerText)
        .then(() => console.log(currentCode, currentFlow.innerText, currentState.innerText))
        .catch(error => console.error(error))
}

btnPauseFlow.onclick = () => {
    if (currentState.innerText == "Active") {
        currentState.innerText = "Paused";
        btnPauseFlow.innerText = "Unpause flow";
    } else {
        currentState.innerText = "Active";
        btnPauseFlow.innerText = "Pause flow";
    }
    SendUpdate();
}