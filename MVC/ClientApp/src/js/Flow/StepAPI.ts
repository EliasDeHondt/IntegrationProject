import { Step } from "./Step/StepObjects";

let nextStepButton = document.getElementById("butNextStep") as HTMLButtonElement;
let informationContainer = document.getElementById("informationContainer") as HTMLDivElement;
let currentStepNumber: number = 0;
let flowId: number;

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

function ShowStep(data: Step) {
    informationContainer.innerHTML = "";
    switch(data.informationViewModel.informationType){
        case "Text": {
            let p = document.createElement("p");
            p.innerText = data.informationViewModel.information;
            informationContainer.appendChild(p);
            break;
        }
        case "Image":{
            let img = document.createElement("img");
            img.src = "data:image/png;base64," + data.informationViewModel.information;
            informationContainer.appendChild(img);
            break;
        }
        case "Video":{
            let video = document.createElement("video");
            video.src = data.informationViewModel.information;
            video.autoplay = true;
            video.loop = true;
            video.controls = false;
            informationContainer.appendChild(video);
            break;
        }
    }
}

nextStepButton.onclick = () => GetNextStep(++currentStepNumber, 1)