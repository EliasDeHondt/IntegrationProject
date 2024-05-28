import * as kiosk from "./Kiosk";
import * as signalR from "@microsoft/signalr";
import {Modal} from "bootstrap";
import {generateQrCode} from "../Util";
import * as stepAPI from "../Flow/StepAPI";
import SignalRConnectionManager from "./ConnectionManager";

const currFlow = document.getElementById("flowId") as HTMLSpanElement;
const modal = new Modal(document.getElementById("pausedFlowModal") as HTMLDivElement, {
    backdrop: 'static',
    keyboard: false
});

let currStateOfFlow = "";
let projectId: number = 0;

const qrcode = document.getElementById("qrcode") as HTMLImageElement;

document.addEventListener("DOMContentLoaded", async () => {
    SignalRConnectionManager.startConnection().then(() => {
        const connection = SignalRConnectionManager.getInstance();
        
        connection.on("ReceiveFlowUpdate", async (id, state) => {
            currStateOfFlow = state;
            if (currStateOfFlow.toLowerCase() == "paused") {
                modal.show()
                stepAPI.stepTimer.pause();
                stepAPI.clockTimer.pause();
            } else {
                modal.hide()
                stepAPI.stepTimer.resume();
                stepAPI.clockTimer.resume();
            }
        })

        connection.on("FlowActivated", (id) => {
            window.location.href = `/Flow/Step/${id}`
        })

        connection.on("CurrentFlowRestarted", async () => {
            await stepAPI.restartFlow()
        })

        connection.on("ReceiveProjectId", (id) => {
            projectId = id;
            let url: string = window.location.hostname;
            generateQrCode(url + `/WebApp/Feed/${projectId}`).then(qr => {
                qrcode.src += qr.replace(new RegExp("\"", 'g'), "");
            })
        })
    })
})