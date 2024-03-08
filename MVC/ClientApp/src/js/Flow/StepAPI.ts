import { Step, Information, Question } from "./Step/StepObjects";

let nextStepButton = document.getElementById("butNextStep") as HTMLButtonElement;
let currentStepNumber: number;
let flowId: number;

let step: Step = {

    StepNumber: 0,
    StepType: "Information",
    Information: {
        GetInformation: ""
    },
    Question: {
        Question: "",
        Choices: []
    }
}

function GetNextStep(stepNumber: number, flowId: number) {

    fetch("/api/Steps/GetNextStep/" + stepNumber + "/" + flowId, {
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

function ShowStep(data: any) {
    console.log(data);
}

nextStepButton.onclick = () => GetNextStep(++currentStepNumber, flowId)