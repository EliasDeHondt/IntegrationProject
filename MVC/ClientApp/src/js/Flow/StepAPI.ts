
import { Step } from "./Step/StepObjects";
import { downloadVideoFromBucket } from "../StorageAPI";
import {Flow} from "./FlowObjects";

const informationContainer = document.getElementById("informationContainer") as HTMLDivElement;
const btnNextStep = document.getElementById("btnNextStep") as HTMLButtonElement;
const btnRestartFlow = document.getElementById("btnRestartFlow") as HTMLButtonElement;
let currentStepNumber: number = 0;
let flowId = Number((document.getElementById("flowId") as HTMLSpanElement).innerText);
let themeId = Number((document.getElementById("theme") as HTMLSpanElement).innerText);

function GetNextStep(stepNumber: number, flowId: number) {
    fetch("/api/Steps/GetNextStep/" + flowId + "/" + stepNumber, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => ShowStep(data))
        .catch(error => console.error("Error:", error))
}

async function ShowStep(data: Step) {
    (document.getElementById("stepNr") as HTMLSpanElement).innerText = currentStepNumber.toString();
    informationContainer.innerHTML = "";
    switch (data.informationViewModel.informationType) {
        case "Text": {
            let p = document.createElement("p");
            p.innerText = data.informationViewModel.information;
            informationContainer.appendChild(p);
            break;
        }
        case "Image": {
            let img = document.createElement("img");
            img.src = "data:image/png;base64," + data.informationViewModel.information;
            informationContainer.appendChild(img);
            break;
        }
        case "Video": {
            let path = await downloadVideoFromBucket(data.informationViewModel.information);
            let video = document.createElement("video");
            if (typeof path === "string") {
                path = path.substring(1, path.length - 1);
                video.src = path;
            }
            video.autoplay = true;
            video.loop = true;
            video.controls = false;
            informationContainer.appendChild(video);
            break;
        }
    }
}
btnNextStep.onclick = () => GetNextStep(++currentStepNumber, flowId)
btnRestartFlow.onclick = () => {
    currentStepNumber = 0;
    GetNextStep(++currentStepNumber, flowId);
};