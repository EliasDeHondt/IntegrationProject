import {Choice, Information, Question, Step} from "../Flow/Step/StepObjects";
import {downloadVideoFromBucket} from "../StorageAPI";
import {Modal} from "bootstrap";
import {Flow, Participation} from "../Flow/FlowObjects";


const stepsList = document.getElementById('steps-list') as HTMLElement;
const btnAddStep = document.getElementById('btn-add-step') as HTMLButtonElement;
const btnViewFlow = document.getElementById('viewFlow') as HTMLButtonElement;

const btnAddChoice = document.getElementById('btn-add-choice') as HTMLButtonElement;
const btnAddText = document.getElementById('btn-add-text') as HTMLButtonElement;
const btnAddImage = document.getElementById('btn-add-image') as HTMLButtonElement;
const btnAddVideo = document.getElementById('btn-add-video') as HTMLButtonElement;
const btnSaveFlow = document.getElementById("saveFlow") as HTMLButtonElement;
const btnAddLink = document.getElementById("btn-add-hyperlink") as HTMLButtonElement;

let currentStepList: Step[] = [];

let currentViewingStep: Step;

let flowId: number;

async function GetSteps(flowId: number): Promise<Step[]> {
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

async function GetStepById(stepNumber: number, flowId: number): Promise<Step> {
    return await fetch("/api/Steps/GetNextStep/" + flowId + "/" + stepNumber, {
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

async function AddChoice(stepNr: number) {
    await fetch(`/EditFlows/CreateChoice/${flowId}/${stepNr}`, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .catch(error => console.error("Error:", error))
}

async function AddInformation(stepNr: number, type: string) {
    await fetch(`/EditFlows/CreateInformation/${flowId}/${stepNr}/${type}`, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .catch(error => console.error("Error:", error))
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

async function UpdateFlow(steps: Step[]) {
    await fetch(`/api/Flows/${flowId}/Update`, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(steps)
    })
}

async function GetConditionalNextStep(stepId: number): Promise<Step> {
    return await fetch(`/api/Steps/GetConditionalNextStep/${stepId}`, {
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

async function GetStepId(stepNr: number): Promise<number> {
    return await fetch(`/EditFlows/GetStepId/${flowId}/${stepNr}`, {
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

function toggleButtons() {
    btnAddText.disabled = true;
    btnAddImage.disabled = true;
    btnAddVideo.disabled = true;
    btnAddChoice.disabled = true;
    btnAddLink.disabled = true;

    if (currentViewingStep == undefined) {
        return;
    }

    if (currentViewingStep.informationViewModel != undefined) {
        btnAddText.disabled = false;
        btnAddImage.disabled = false;
        btnAddVideo.disabled = false;
        btnAddLink.disabled = false;
        if (currentViewingStep.informationViewModel.length >= 2) {
            btnAddText.disabled = true;
            btnAddImage.disabled = true;
            btnAddVideo.disabled = true;
            btnAddLink.disabled = true;
        }
    }
    if (currentViewingStep.questionViewModel != undefined && currentViewingStep.questionViewModel.questionType != "OpenQuestion") {
        btnAddChoice.disabled = false;
        if (currentViewingStep.questionViewModel.choices.length >= 6)
            btnAddChoice.disabled = true;
    }


}

function UpdateStepList(steps: Step[]) {
    currentStepList = [];
    GetSteps(flowId).then(steps => steps.forEach(step => {
        GetStepById(step.stepNumber, flowId)
            .then(s => currentStepList.push(s));
    }))
    currentStepList.sort((a, b) => a.stepNumber - b.stepNumber);
    steps.sort((a, b) => a.stepNumber - b.stepNumber);

    console.log(currentStepList);

    const stepsList = document.getElementById("steps-list") as HTMLDivElement;

    stepsList.innerHTML = "";

    if (steps.length > 0) {
        steps.forEach(step => {
            //Card container
            const stepCard = document.createElement('a');
            stepCard.classList.add("step-card", "btn", "justify-content-center", "align-items-center");
            stepCard.dataset.stepNumber = step.stepNumber.toString();
            //Card Header
            const cardHeader = document.createElement('h2');
            cardHeader.classList.add("step-card-header");
            cardHeader.innerText = "Step " + step.stepNumber.toString();
            GetStepById(step.stepNumber, flowId).then(s => {
                if (s.questionViewModel != undefined)
                    cardHeader.innerText += "\n" + s.questionViewModel.questionType.toString();
                if (s.informationViewModel != undefined)
                    cardHeader.innerText += "\nInformation"
            })

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
    const btnViewFlow = document.getElementById("viewFlow") as HTMLAnchorElement;
    flowId = parseInt(lastPart, 10);

    if (isNaN(flowId)) {
        console.error("The ID provided in the URL is not a number.")
    }

    // GetSteps(flowId).then(() => { 
    //   initializeCardLinks();
    //   btnViewFlow.setAttribute('href', "/Flow/Step/" + flowId);
    // });
    GetSteps(flowId)
        .then(steps => UpdateStepList(steps))
        .then(() => toggleButtons())
        .then(() => initializeCardLinks())
});

/* This function checks if the provided URL is valid */
function checkURLValidity(url: string): boolean {
    let urlRegex = /(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?\/[a-zA-Z0-9]{2,}|((https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?)|(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}(\.[a-zA-Z0-9]{2,})?/g;

    return urlRegex.test(url);
}

btnAddChoice.addEventListener('click', async () => {
    currentViewingStep.questionViewModel.choices.length++

    if (currentViewingStep.questionViewModel.choices.length >= 6) {
        btnAddChoice.disabled = true;
    }
    await AddChoice(currentViewingStep.stepNumber);
    let div = document.createElement("div");
    div.classList.add("text-start", "m-auto", "choice-container")

    const textArea = document.createElement("input");
    textArea.classList.add("choice-textarea")

    const select = document.createElement("select");
    select.classList.add("choice-select");
    fillConditionalPoints(select, undefined);

    let choiceContainer = document.getElementById("choices-container") as HTMLDivElement;

    div.append(textArea);
    div.append(select);
    choiceContainer.appendChild(div);
});
btnAddText.addEventListener('click', async () => {
    currentViewingStep.informationViewModel.length++
    if (currentViewingStep.informationViewModel.length >= 2) {
        btnAddText.disabled = true;
        btnAddImage.disabled = true;
        btnAddVideo.disabled = true;
    }
    await AddInformation(currentViewingStep.stepNumber, "Text");
    let div = document.createElement("div");
    div.id = "text-container"
    div.classList.add("text-start", "m-auto")

    const textArea = document.createElement("textarea");
    textArea.id = "text-textarea"

    div.append(textArea);
    partialInformationContainer.appendChild(div);
});

btnAddLink.addEventListener('click', () => {

    currentViewingStep.informationViewModel.length++
    toggleButtons();

    let div = document.createElement("div");
    div.id = "text-container";
    div.classList.add("text-start", "m-auto");

    const textArea = document.createElement("textarea");
    textArea.classList.add("input-hyperlink-iframe");
    textArea.addEventListener('change', async () => {
        let isWorkingURL = checkURLValidity(textArea.value)
        if (isWorkingURL) {
            textArea.dataset.allowed = String(1);
            textArea.classList.add("hyperlink-allowed");
        } else {
            textArea.dataset.allowed = String(0);
            textArea.classList.add("hyperlink-disallowed");
        }
    })

    div.append(textArea);
    partialInformationContainer.appendChild(div);
})

btnAddImage.addEventListener('click', () => {
    //TODO: add image logica
});

btnAddVideo.addEventListener('click', () => {
    //TODO: add video logica
});
btnSaveFlow.addEventListener('click', async () => {
    await GetStepData();
    GetFlowById(flowId)
        .then(flow => GetNewFlowData(flow))
        .then(flow => {
            console.log(flow.steps);
            UpdateFlow(flow.steps);
        })
});

async function GetStepData() {
    if (!currentViewingStep) {
        return;
    }


    const index = currentStepList.findIndex(step => step.stepNumber === currentViewingStep.stepNumber);

    if (currentViewingStep.informationViewModel) {
        let informationArray: Information[] = currentViewingStep.informationViewModel;
        const contentElements = partialInformationContainer.children;

        for (let i = 0; i < contentElements.length - 1; i++) {
            const element = contentElements[i] as HTMLElement;

            let information: string = "";
            let informationType: string = "";

            switch (element.tagName) {
                case 'TEXTAREA':
                    const textAreaElement = element as HTMLTextAreaElement;
                    information = textAreaElement.value;
                    console.log(textAreaElement.value)
                    informationType = 'Text';
                    break;
                case 'IMG':
                    const imageElement = element as HTMLImageElement;
                    information = imageElement.src.replace("data:image/png;base64,", "");
                    informationType = 'Image';
                    break;
                case 'VIDEO':
                    const videoElement = element as HTMLVideoElement;
                    information = videoElement.src;
                    informationType = 'Video';
                    break;
                case 'IFRAME':
                    const linkTextAreaElement = element as HTMLTextAreaElement;
                    information = linkTextAreaElement.value;
                    informationType = 'Hyperlink'
                    break;
            }
            informationArray[i] = {
                id: informationArray[i].id,
                information: information,
                informationType: informationType
            };
        }
        if (index !== -1) {
            currentStepList[index].informationViewModel = informationArray;
            currentViewingStep.informationViewModel = informationArray;
        }
    }

    if (currentViewingStep.questionViewModel) {
        let choicesArray: Choice[] = currentViewingStep.questionViewModel.choices;

        const questionContainer = partialQuestionContainer.children[0] as HTMLDivElement;
        const choicesContainer = partialQuestionContainer.children[1] as HTMLDivElement;

        for (let i = 0; i < choicesContainer.children.length - 1; i++) {
            const element = choicesContainer.children[i].children[0] as HTMLInputElement;
            const select = choicesContainer.children[i].children[1] as HTMLSelectElement;
            let nextStepId: number = 0;
            let selectedOption = select.options[select.selectedIndex];
            if (selectedOption.value != "undefined") {
                choicesArray[i] = {
                    id: choicesArray[i].id,
                    text: element.value,
                    nextStepId: parseInt(selectedOption.value)
                };
            } else {
                choicesArray[i] = {id: choicesArray[i].id, text: element.value, nextStepId: undefined};
            }
        }
        const questionElement = questionContainer.children[0] as HTMLTextAreaElement;

        let questionText = questionElement.value;
        let questionType = currentViewingStep.questionViewModel.questionType;
        let question: Question = {
            id: currentStepList[index].questionViewModel.id,
            question: questionText,
            questionType: questionType,
            choices: choicesArray
        }

        if (index !== -1) {
            currentStepList[index].questionViewModel = question;
            currentViewingStep.questionViewModel = question;
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
            id: step.id,
            stepNumber: step.stepNumber,
            informationViewModel: step.informationViewModel,
            questionViewModel: step.questionViewModel,
            notes: step.notes
        };

        if (step.questionViewModel && step.questionViewModel.questionType !== "OpenQuestion") {
            const choices: Choice[] = [];
            for (const choice of step.questionViewModel.choices) {
                choices.push({id: choice.id, text: choice.text, nextStepId: choice.nextStepId});
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
        stepCard.addEventListener('click', async () => {
            await GetStepData();
            const stepNumber = stepCard.dataset.stepNumber;

            if (stepNumber) {
                GetStepById(parseInt(stepNumber), flowId).then(s => {
                    ShowStepInContainer(s.stepNumber);
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

async function ShowStepInContainer(stepNr: number) {
    let stepIndex = currentStepList.findIndex(s => s.stepNumber == stepNr);
    let data = currentStepList[stepIndex];
    partialInformationContainer.innerHTML = "";
    partialQuestionContainer.innerHTML = "";
    toggleButtons();
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
                    img.classList.add("col-m-12");
                    img.style.width = '600px'; //schalen image
                    img.style.height = '600px';
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
                case "Hyperlink": {
                    let url = infoStep.information;
                    let iframe = document.createElement("textarea");
                    iframe.value = url;
                    iframe.classList.add("input-hyperlink-iframe");
                    iframe.addEventListener('change', async () => {
                        let isWorkingURL = checkURLValidity(iframe.value)
                        if (isWorkingURL) {
                            iframe.dataset.allowed = String(1);
                            iframe.classList.remove("hyperlink-disallowed");
                            iframe.classList.add("hyperlink-allowed");
                        } else {
                            iframe.dataset.allowed = String(0);
                            iframe.classList.remove("hyperlink-allowed");
                            iframe.classList.add("hyperlink-disallowed");
                        }
                    })
                    partialInformationContainer.appendChild(iframe);
                    break;
                }
            }
        }
    }

    if (data.questionViewModel != undefined) {
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
        switch (data.questionViewModel.questionType) {
            case "SingleChoiceQuestion":
                for (const element of data.questionViewModel.choices) {
                    let div = document.createElement("div");
                    div.classList.add("text-start", "m-auto", "choice-container")

                    const textArea = document.createElement("input");
                    textArea.classList.add("choice-textarea")
                    textArea.value = element.text;

                    const select = document.createElement("select");
                    select.classList.add("choice-select");
                    fillConditionalPoints(select, element.nextStepId);

                    div.append(textArea);
                    div.append(select);
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

                    const select = document.createElement("select");
                    select.classList.add("choice-select");
                    fillConditionalPoints(select, element.nextStepId);

                    div.append(textArea);
                    div.append(select);
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

                    const select = document.createElement("select");
                    select.classList.add("choice-select");
                    fillConditionalPoints(select, element.nextStepId);

                    div.append(textArea);
                    div.append(select);
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
                break;
        }
        partialQuestionContainer.appendChild(choiceContainer);
    }
}

function fillConditionalPoints(select: HTMLSelectElement, nextStepId?: number) {
    let option = document.createElement("option");
    option.value = "undefined";
    option.innerText = "Next step"
    option.selected = true;
    select.appendChild(option);
    GetSteps(flowId).then(async steps => {
        steps.sort((a, b) => a.stepNumber - b.stepNumber);
        for (const step of steps) {
            let option = document.createElement("option");
            await GetStepId(step.stepNumber).then(id => option.value = id.toString());
            option.innerText = `Step ${step.stepNumber}`
            select.appendChild(option);
            if (parseInt(option.value) == nextStepId)
                option.selected = true;
        }
    })
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
    currentStepList.sort((a, b) => a.stepNumber - b.stepNumber);
    let newStepNumber = currentStepList[currentStepList.length - 1].stepNumber + 1
    if (infographic.checked) {
        AddStep(newStepNumber, "Information")
            .then(() => GetSteps(flowId).then(steps => UpdateStepList(steps)))
            .then(() => initializeCardLinks());
    } else if (singleQ.checked) {
        AddStep(newStepNumber, "Single Choice Question")
            .then(() => GetSteps(flowId).then(steps => UpdateStepList(steps)))
            .then(() => initializeCardLinks());
    } else if (multipleQ.checked) {
        AddStep(newStepNumber, "Multiple Choice Question")
            .then(() => GetSteps(flowId).then(steps => UpdateStepList(steps)))
            .then(() => initializeCardLinks());
    } else if (rangeQ.checked) {
        AddStep(newStepNumber, "Ranged Question")
            .then(() => GetSteps(flowId).then(steps => UpdateStepList(steps)))
            .then(() => initializeCardLinks());
    } else if (openQ.checked) {
        AddStep(newStepNumber, "Open Question")
            .then(() => GetSteps(flowId).then(steps => UpdateStepList(steps)))
            .then(() => initializeCardLinks());
    }
    clearModal()
}

function clearModal() {
    CreateStepModal.hide();
    ViewStepModal.hide();
}

const btnCancelViewFlowModal = document.getElementById("btnCancelViewFlowModal") as HTMLButtonElement;
const btnSaveViewFlowModal = document.getElementById("btnSaveViewFlowModal") as HTMLButtonElement;
const btnConfirmViewFlow = document.getElementById("btnConfirmViewFlow") as HTMLAnchorElement;

const ViewStepModal = new Modal(document.getElementById('ViewFlowModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

btnViewFlow.onclick = () => {
    ViewStepModal.show();
    btnConfirmViewFlow.setAttribute('href', "/Flow/Step/" + flowId);
}

btnCancelViewFlowModal.onclick = () => {
    clearModal();
}

btnSaveViewFlowModal.onclick = () => {
    //TODO: after merge with Jana, add saveFlow
    //saveFlow();
}

btnConfirmViewFlow.onclick = () => {
    clearModal();
}