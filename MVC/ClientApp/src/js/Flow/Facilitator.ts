import * as signalR from "@microsoft/signalr";

let currentFlow = document.getElementById("currentFlow") as HTMLHeadingElement;
let currentState = document.getElementById("currentState") as HTMLSpanElement;

export const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

export const connectionStart = connection.start()
    .then(() => {
        console.log("SignalR connection started successfully.");
    })
    .catch((err) => {
        console.error("Error starting SignalR connection: " + err);
    });

document.addEventListener("DOMContentLoaded", function () {
    const btnEnterCode = document.getElementById("btnEnterCode") as HTMLInputElement;
    if (btnEnterCode)
        btnEnterCode.onclick = () => {
            const code = (document.getElementById("inputCode") as HTMLInputElement).value;
            connection.invoke("ConnectToUser", code)
                .then(() => {
                    console.log("Connected to installation #" + code);
                })
                .catch((err) => {
                    console.error("Error user connection: " + err + code);
                });
        };
});

connection.on("ReceiveFlowUpdate", (id, state) => {
    console.log(id, state)
    currentFlow.innerText = `${id}`
    currentState.innerText = `${state}`
})

