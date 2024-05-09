import {Choice, Information, Question, Step} from "../Flow/Step/StepObjects";
import {downloadVideoFromBucket} from "../StorageAPI";
import {Modal} from "bootstrap";
import {Flow, Participation} from "../Flow/FlowObjects";

const stepsList = document.getElementById('steps-list') as HTMLElement;
const btnAddStep = document.getElementById('btn-add-step') as HTMLButtonElement;

const btnAddChoice = document.getElementById('btn-add-choice') as HTMLButtonElement;
const btnAddText = document.getElementById('btn-add-text') as HTMLButtonElement;
const btnAddImage = document.getElementById('btn-add-image') as HTMLButtonElement;
const btnAddVideo = document.getElementById('btn-add-video') as HTMLButtonElement;
const btnSaveFlow = document.getElementById("saveFlow") as HTMLButtonElement;

let currentStepList: Step[];

let currentViewingStep: Step;

let flowId: number;

async function GetSteps(flowId: number): Promise<Step[]> {
    console.log("Fetching steps...");
    return await fetch(`/EditFlows/GetSteps/${flowId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data
        })
}

function GetStepById(stepNumber: number, flowId: number): Promise<Step> {
    return fetch("/api/Steps/GetNextStep/" + flowId + "/" + stepNumber, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
}

async function AddStep(stepNumber: number, stepType: string) {
    await fetch("/EditFlows/CreateStep/" + flowId + "/" + stepNumber + "/" + stepType, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .catch(error => console.error("Error:", error))
}

async function UpdateStep(flowId: number, stepNr: number, step: Step) {
    await fetch(`/api/Steps/${flowId}/Update/${step.stepNumber}`, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(step)
    })
}

async function GetFlowById(flowId: number): Promise<Flow> {
    return await fetch(`/api/Flows/${flowId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
}

async function UpdateFlow(flow: Flow) {
    await fetch(`/api/Flows/${flow.id}/Update`, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(flow)
    })
}

function toggleButtons() {

    console.log("disabling all buttons")
    btnAddText.disabled = true;
    btnAddImage.disabled = true;
    btnAddVideo.disabled = true;
    btnAddChoice.disabled = true;

    if (currentViewingStep == undefined) {
        return;
    }

    if (currentViewingStep.informationViewModel != undefined) {
        console.log("enabling all info buttons")
        btnAddText.disabled = false;
        btnAddImage.disabled = false;
        btnAddVideo.disabled = false;
    }
    if (currentViewingStep.questionViewModel != undefined && currentViewingStep.questionViewModel.questionType != "OpenQuestion") {
        console.log("enabling all question buttons")
        btnAddChoice.disabled = false;
    }
}

function UpdateStepList(steps: Step[]) {

    steps.sort((a, b) => a.stepNumber - b.stepNumber);

    currentStepList = steps;

    console.log(currentStepList)

    const stepsList = document.getElementById("steps-list") as HTMLDivElement;

    stepsList.innerHTML = "";

    if (steps.length > 0) {
        steps.forEach(step => {

            //Card container
            const stepCard = document.createElement('a');
            stepCard.classList.add("step-card", "btn");
            stepCard.dataset.stepNumber = step.stepNumber.toString();
            //Card Header
            const cardHeader = document.createElement('h2');
            cardHeader.classList.add("step-card-header");
            cardHeader.innerText = "Step " + step.stepNumber.toString();

            stepCard.appendChild(cardHeader);

            stepsList.appendChild(stepCard);
        })
    } else {
        const noFlowsMessage = document.createElement('p');
        noFlowsMessage.classList.add('no-cards-message');
        noFlowsMessage.textContent = 'There are currently no Steps in this flow!';
        stepsList.appendChild(noFlowsMessage);
    }

}

document.addEventListener('DOMContentLoaded', () => {
    const parts = document.URL.split('/');
    const lastPart = parts[parts.length - 1];
    flowId = parseInt(lastPart, 10);

    if (isNaN(flowId)) {
        console.error("The ID provided in the URL is not a number.")
    }

    GetSteps(flowId)
        .then(steps => UpdateStepList(steps))
        .then(() => toggleButtons())
        .then(() => initializeCardLinks())
});

btnAddChoice.addEventListener('click', () => {
    console.log("Add choice")
    let div = document.createElement("div");
    div.classList.add("text-start", "m-auto", "choice-container")

    const textArea = document.createElement("input");
    textArea.classList.add("choice-textarea")

    let choiceContainer = document.getElementById("choices-container") as HTMLDivElement;

    div.append(textArea);
    choiceContainer.appendChild(div);
});

btnAddText.addEventListener('click', () => {
    console.log("Add text")
    let div = document.createElement("div");
    div.id = "text-container"
    div.classList.add("text-start", "m-auto")

    const textArea = document.createElement("textarea");
    textArea.id = "text-textarea"

    div.append(textArea);
    partialInformationContainer.appendChild(div);
});

btnAddImage.addEventListener('click', () => {
    console.log("Add Image")
    //TODO: add image logica
});

btnAddVideo.addEventListener('click', () => {
    console.log("Add Video")
    //TODO: add video logica
});

btnSaveFlow.addEventListener('click', () => {
    console.log("Saving flow...");
    GetStepData();
    GetFlowById(flowId)
        .then(flow => GetNewFlowData(flow))
        .then(flow => {
            UpdateFlow(flow);
        })
});


function GetStepData() {
    if (!currentViewingStep) {
        return;
    }

    let informationArray: Information[] = [];
    let choicesArray: Choice[] = [];

    const index = currentStepList.findIndex(step => step.stepNumber === currentViewingStep.stepNumber);

    if (currentViewingStep.informationViewModel) {
        const contentElements = partialInformationContainer.children;

        for (let i = 0; i < contentElements.length; i++) {
            const element = contentElements[i] as HTMLElement;

            let information: string = "";
            let informationType: string = "";

            switch (element.tagName) {
                case 'TEXTAREA':
                    const textAreaElement = element as HTMLTextAreaElement;
                    information = textAreaElement.value;
                    informationType = 'Text';
                    break;
                case 'IMG':
                    const imageElement = element as HTMLImageElement;
                    information = imageElement.src;
                    informationType = 'Image';
                    break;
                case 'VIDEO':
                    const videoElement = element as HTMLVideoElement;
                    information = videoElement.src;
                    informationType = 'Video';
                    break;
            }

            informationArray.push({information: information, informationType: informationType});
        }
        if (index !== -1) {
            currentStepList[index].informationViewModel = informationArray;
        }
    }

    if (currentViewingStep.questionViewModel) {
        const questionContainer = partialQuestionContainer.children[1] as HTMLDivElement;
        const choicesContainer = partialQuestionContainer.children[2] as HTMLDivElement;

        for (let i = 0; i < choicesContainer.children.length; i++) {
            const element = choicesContainer.children[i].children[0] as HTMLInputElement;
            choicesArray.push({text: element.value});
        }

        const questionElement = questionContainer.children[0] as HTMLTextAreaElement;

        let questionText = questionElement.value;
        let questionType = partialQuestionContainer.children[0].innerHTML;

        let question: Question = {question: questionText, questionType: questionType, choices: choicesArray}

        if (index !== -1) {
            currentStepList[index].questionViewModel = question;
        }
    }
}

async function GetNewFlowData(flow: Flow): Promise<Flow> {
    let flowData: Flow = {
        id: flow.id,
        flowType: flow.flowType,
        steps: [] as Step[],
        participations: [] as Participation[]
    }

    for (const step of currentStepList) {
        let stepData: Step = {
            stepNumber: step.stepNumber,
            informationViewModel: step.informationViewModel,
            questionViewModel: step.questionViewModel
        };

        if (step.questionViewModel && step.questionViewModel.questionType !== "OpenQuestion") {
            const choices: Choice[] = [];
            for (const choice of step.questionViewModel.choices) {
                choices.push({text: choice.text});
            }
            if (stepData.questionViewModel)
                stepData.questionViewModel.choices = choices;
        }

        flowData.steps.push(stepData);
    }

    return flowData;
}

function initializeCardLinks() {
    let stepCards = document.querySelectorAll('.step-card') as NodeListOf<HTMLAnchorElement>;

    stepCards.forEach(stepCard => {
        stepCard.addEventListener('click', () => {
            const stepNumber = stepCard.dataset.stepNumber;

            if (stepNumber) {
                GetStepById(parseInt(stepNumber), flowId).then(s => {
                    ShowStepInContainer(s);
                    currentViewingStep = s;
                    toggleButtons();
                });
            } else {
                console.error('Step ID not defined in dataset');
            }
        });
    });
}

const partialInformationContainer = document.getElementById("partialInformationContainer") as HTMLDivElement;
const partialQuestionContainer = document.getElementById("partialQuestionContainer") as HTMLDivElement;

async function ShowStepInContainer(data: Step) {
    partialInformationContainer.innerHTML = "";
    partialQuestionContainer.innerHTML = "";
    toggleButtons();
    console.log(data)
    if (data.informationViewModel != undefined) {
        for (const infoStep of data.informationViewModel) {
            switch (infoStep.informationType) {
                case "Text": {
                    let textArea = document.createElement("textarea");
                    textArea.id = "text-textarea"
                    textArea.value = infoStep.information;

                    partialInformationContainer.appendChild(textArea);
                }
                    break;
                case "Image": {
                    let img = document.createElement("img");
                    img.src = "data:image/png;base64," + infoStep.information;
                    img.classList.add("col-m-12", "w-100", "h-100");
                    partialInformationContainer.appendChild(img);
                    break;
                }
                case "Video": {
                    let path = await downloadVideoFromBucket(infoStep.information);
                    let video = document.createElement("video");
                    if (typeof path === "string") {
                        path = path.substring(1, path.length - 1);
                        video.src = path;
                    }
                    video.autoplay = true;
                    video.loop = true;
                    video.controls = false;
                    video.classList.add("h-100", "w-100");
                    partialInformationContainer.appendChild(video);
                    break;
                }
            }
        }
    }

    if (data.questionViewModel != undefined) {
        let questionType = document.createElement('p');
        partialQuestionContainer.appendChild(questionType);
        let questionContainer = document.createElement("div");
        questionContainer.id = "question-container"
        questionContainer.classList.add("text-start", "m-auto");

        const textArea = document.createElement("textarea");
        textArea.id = "question-textarea"
        textArea.value = data.questionViewModel.question;

        questionContainer.append(textArea);
        partialQuestionContainer.appendChild(questionContainer);

        let choiceContainer = document.createElement('div');
        choiceContainer.id = "choices-container";
        questionType.innerText = data.questionViewModel.questionType;
        switch (data.questionViewModel.questionType) {
            case "SingleChoiceQuestion":
                for (const element of data.questionViewModel.choices) {
                    let div = document.createElement("div");
                    div.classList.add("text-start", "m-auto", "choice-container")

                    const textArea = document.createElement("input");
                    textArea.classList.add("choice-textarea")
                    textArea.value = element.text;

                    div.append(textArea);
                    choiceContainer.appendChild(div);
                }
                break;
            case "MultipleChoiceQuestion":
                for (const element of data.questionViewModel.choices) {
                    let div = document.createElement("div");
                    div.classList.add("text-start", "m-auto", "choice-container")

                    const textArea = document.createElement("input");
                    textArea.classList.add("choice-textarea")
                    textArea.value = element.text;

                    div.append(textArea);
                    choiceContainer.appendChild(div);
                }
                break;
            case "RangeQuestion":
                for (const element of data.questionViewModel.choices) {
                    let div = document.createElement("div");
                    div.classList.add("text-start", "m-auto", "choice-container")

                    const textArea = document.createElement("input");
                    textArea.classList.add("choice-textarea")
                    textArea.value = element.text;

                    div.append(textArea);
                    choiceContainer.appendChild(div);
                }
                break;
            case "OpenQuestion": {
                let div = document.createElement("div");
                div.classList.add("m-auto");

                partialQuestionContainer.appendChild(div);
                break;
            }
            default:
                console.log("This question type is not currently supported. (QuestionType: " + data.questionViewModel.questionType);
                break;
        }
        partialQuestionContainer.appendChild(choiceContainer);
    }
}

const CreateStepModal = new Modal(document.getElementById('CreateStepModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

btnAddStep.onclick = () => {
    CreateStepModal.show();
}


const butCloseCreateStep = document.getElementById("butCloseCreateStep") as HTMLButtonElement;
const butCancelCreateStep = document.getElementById("butCancelCreateStep") as HTMLButtonElement;
const butConfirmCreateStep = document.getElementById("butConfirmCreateStep") as HTMLButtonElement;

const infographic = document.getElementById("infographic") as HTMLInputElement;
const singleQ = document.getElementById("singleQ") as HTMLInputElement;
const multipleQ = document.getElementById("multipleQ") as HTMLInputElement;
const rangeQ = document.getElementById("rangeQ") as HTMLInputElement;
const openQ = document.getElementById("openQ") as HTMLInputElement;

butCancelCreateStep.onclick = () => {
    clearModal()
}

butCloseCreateStep.onclick = () => {
    clearModal()
}
butConfirmCreateStep.onclick = () => {
  let newStepNumber = currentStepList[currentStepList.length - 1].stepNumber + 1
  if(infographic.checked){
    AddStep(newStepNumber,"Information")
      .then(() => GetSteps(flowId))
      .then(() => initializeCardLinks());
  } else if (singleQ.checked) {
    AddStep(newStepNumber, "Single Choice Question")
        .then(() => GetSteps(flowId))
        .then(() => initializeCardLinks());
  } else if (multipleQ.checked) {
    AddStep(newStepNumber, "Multiple Choice Question")
        .then(() => GetSteps(flowId))
        .then(() => initializeCardLinks());
  } else if (rangeQ.checked) {
    AddStep(newStepNumber, "Ranged Question")
        .then(() => GetSteps(flowId))
        .then(() => initializeCardLinks());
  } else if (openQ.checked) {
    AddStep(newStepNumber, "Open Question")
        .then(() => GetSteps(flowId))
        .then(() => initializeCardLinks());
  }
  clearModal()
}

function clearModal() {
    CreateStepModal.hide();
}