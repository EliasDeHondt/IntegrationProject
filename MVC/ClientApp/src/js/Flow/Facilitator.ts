import * as signalR from "@microsoft/signalr";

const currentFlow = document.getElementById("currentFlow") as HTMLHeadingElement;

export const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("ReceiveFlowState", (id, state) => {
    currentFlow.innerHTML = `${id}`;
});

connection.start()
    .then(() => {
        console.log("SignalR connection started successfully.");
    })
    .catch((err) => {
        console.error("Error starting SignalR connection: " + err);
    });