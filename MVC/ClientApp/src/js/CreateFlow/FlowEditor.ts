import {
    Choice,
    Information, InformationStep,
    isInformationStep,
    isQuestionStep,
    Question,
    QuestionStep,
    Step
} from "../Flow/Step/StepObjects";
import {downloadVideoFromBucket, uploadVideoToBucket} from "../StorageAPI";
import {Modal, Toast} from "bootstrap";
import {Flow, Participation} from "../Flow/FlowObjects";
import {readFileAsBase64} from "../Util";

const saveFlowToast = new Toast(document.getElementById("saveFlowToast")!);

const btnSaveFlow = document.getElementById("saveFlow") as HTMLButtonElement;
const btnViewFlow = document.getElementById('viewFlow') as HTMLButtonElement;
const btnStepVisibility = document.getElementById("btn-step-visibility") as HTMLButtonElement;
const btnAddChoice = document.getElementById('btn-add-choice') as HTMLButtonElement;
const btnAddText = document.getElementById('btn-add-text') as HTMLButtonElement;
const btnAddImage = document.getElementById('btn-add-image') as HTMLButtonElement;
const btnAddVideo = document.getElementById('btn-add-video') as HTMLButtonElement;
const btnAddLink = document.getElementById("btn-add-hyperlink") as HTMLButtonElement;
const btnAddStep = document.getElementById('btn-add-step') as HTMLButtonElement;

const divInformation = document.getElementById("partialInformationContainer") as HTMLDivElement;
const divQuestion = document.getElementById("partialQuestionContainer") as HTMLDivElement;

let currentStep: Step;
let currentStepList: Step[] = [];
let flowId: number;

async function GetStepsFromFlow(flowId: number): Promise<Step[]> {
    return await fetch(`/api/Steps/GetStepsFromFlow/${flowId}`, {
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

async function GetStepByNumber(flowId: number, stepNr: number): Promise<Step> {
    return await fetch(`/api/Steps/GetNextStep/${flowId}/${stepNr}`, {
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

async function UpdateInformationStep(step: Step) {
    await fetch(`/api/Steps/UpdateInformationStep`, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(step)
    })
}

async function UpdateQuestionStep(step: Step) {
    await fetch(`/api/Steps/UpdateQuestionStep`, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(step)
    })
}


document.addEventListener("DOMContentLoaded", () => {
    flowId = getFlowId();
    GetStepsFromFlow(flowId)
        .then(steps => updateStepList(steps))
        .then(() => initializeCardLinks())
        .then(() => toggleButtons());
})

btnAddChoice.onclick = async () => {
    await AddChoice(currentStep.stepNumber)
        .then(() => GetStepsFromFlow(flowId))
        .then(() => updateStepList(currentStepList))
        .then(() => initializeCardLinks());
    let index = currentStepList.findIndex(s => s.stepNumber == currentStep.stepNumber);
    await showStepInContainer(currentStepList[index]);
}

btnAddText.onclick = async () => {
    await AddInformation(currentStep.stepNumber, 'Text')
        .then(() => GetStepsFromFlow(flowId))
        .then(() => updateStepList(currentStepList))
        .then(() => initializeCardLinks());
    let index = currentStepList.findIndex(s => s.stepNumber == currentStep.stepNumber);
    await showStepInContainer(currentStepList[index]);
}

btnAddLink.onclick = async () => {
    await AddInformation(currentStep.stepNumber, 'Hyperlink')
        .then(() => GetStepsFromFlow(flowId))
        .then(() => updateStepList(currentStepList))
        .then(() => initializeCardLinks());
    let index = currentStepList.findIndex(s => s.stepNumber == currentStep.stepNumber);
    await showStepInContainer(currentStepList[index]);
}

btnAddImage.onclick = async () => {
    await AddInformation(currentStep.stepNumber, 'Image')
        .then(() => GetStepsFromFlow(flowId))
        .then(() => updateStepList(currentStepList))
        .then(() => initializeCardLinks());
    let index = currentStepList.findIndex(s => s.stepNumber == currentStep.stepNumber);
    await showStepInContainer(currentStepList[index]);
}

btnAddVideo.onclick = async () => {
    await AddInformation(currentStep.stepNumber, 'Video')
        .then(() => GetStepsFromFlow(flowId))
        .then(() => updateStepList(currentStepList))
        .then(() => initializeCardLinks());
    let index = currentStepList.findIndex(s => s.stepNumber == currentStep.stepNumber);
    await showStepInContainer(currentStepList[index]);
}

async function saveFlow(){
    await readStepInContainer();
    for (const step of currentStepList) {
        if (isQuestionStep(step))
            await UpdateQuestionStep(step);
        if (isInformationStep(step))
            await UpdateInformationStep(step);
    }
    saveFlowToast.show();

    let closeSaveFlowToast = document.getElementById("closeSaveFlowToast") as HTMLButtonElement
    closeSaveFlowToast.onclick = () => saveFlowToast.hide()
}

btnSaveFlow.onclick = async () => {
    saveFlow()
}

function toggleButtons() {
    btnAddText.disabled = true;
    btnAddImage.disabled = true;
    btnAddVideo.disabled = true;
    btnAddChoice.disabled = true;
    btnAddLink.disabled = true;
    btnStepVisibility.disabled = true;

    if (!currentStep)
        return;

    if (isInformationStep(currentStep)) {
        btnAddText.disabled = false;
        btnAddImage.disabled = false;
        btnAddVideo.disabled = false;
        btnAddLink.disabled = false
        if (currentStep.informationViewModel.length >= 2) {
            btnAddText.disabled = true;
            btnAddImage.disabled = true;
            btnAddVideo.disabled = true;
            btnAddLink.disabled = true;
        }
    }
    if (isQuestionStep(currentStep) && currentStep.questionViewModel.questionType != "OpenQuestion") {
        btnAddChoice.disabled = false;
        if (currentStep.questionViewModel.choices.length >= 6)
            btnAddChoice.disabled = true;
    }

    if (isInformationStep(currentStep) || isQuestionStep(currentStep))
        btnStepVisibility.disabled = false;

    const index = currentStepList.findIndex(s => s.stepNumber === currentStep.stepNumber);

    if (!currentStepList[index].visible) {
        btnStepVisibility.innerText = 'Enable Step'
    } else {
        btnStepVisibility.innerText = 'Disable Step';
    }
}

function showStepVisibility(step: Step) {
    let stepCards = document.querySelectorAll('.step-card') as NodeListOf<HTMLAnchorElement>;

    stepCards.forEach(stepCard => {
        if (stepCard.dataset.stepNumber == step.stepNumber.toString()) {
            if (!step.visible) {
                stepCard.classList.add('step-card-hidden')
            } else {
                stepCard.classList.remove('step-card-hidden')
            }
        }
    });
}

async function updateStepList(steps: Step[]) {
    for (const step of steps) {
        await GetStepByNumber(flowId, step.stepNumber).then(s => currentStepList[s.stepNumber - 1] = s);
    }

    console.log(currentStepList);

    const stepsList = document.getElementById("steps-list") as HTMLDivElement;

    stepsList.innerHTML = "";

    if (currentStepList.length > 0) {
        currentStepList.forEach(step => {
            const stepCard = document.createElement('a');
            stepCard.classList.add("step-card", "btn", "justify-content-center", "align-items-center");
            stepCard.dataset.stepNumber = step.stepNumber.toString();

            const cardHeader = document.createElement('h2');
            cardHeader.classList.add("step-card-header");
            cardHeader.innerText = "Step " + step.stepNumber.toString();

            if (isQuestionStep(step))
                cardHeader.innerText += "\n" + step.questionViewModel.questionType.toString();
            if (isInformationStep(step))
                cardHeader.innerText += "\nInformation"

            if (!step.visible)
                stepCard.classList.add('step-card-hidden')

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

function getFlowId(): number {
    let href = window.location.href;
    let regex = RegExp(/\/EditFlow\/FlowEditor\/(\d+)/).exec(href);

    if (regex)
        return parseInt(regex[1], 10);

    return 0;
}

function initializeCardLinks() {
    let stepCards = document.querySelectorAll('.step-card') as NodeListOf<HTMLAnchorElement>;

    stepCards.forEach(stepCard => {
        stepCard.addEventListener('click', async () => {
            await readStepInContainer();
            const stepNumber = stepCard.dataset.stepNumber;

            if (stepNumber) {
                let index = currentStepList.findIndex(s => s.stepNumber == parseInt(stepNumber));
                await showStepInContainer(currentStepList[index])
            }
        });
    });
}

async function readStepInContainer() {
    if (!currentStep)
        return;

    let index = currentStepList.findIndex(s => s.stepNumber == currentStep.stepNumber);

    if (isInformationStep(currentStep)) {
        let elements = divInformation.children;
        let infoArray: Information[] = currentStep.informationViewModel;
        const elementsArray = Array.from(elements) as HTMLElement[];
        elementsArray.forEach(element => {
            if (element.tagName.toLowerCase() === 'input') {
                if (element.parentNode) {
                    element.parentNode.removeChild(element);
                }
            }
        });
        elements = divInformation.children;
        for (let i = 0; i < elements.length; i++) {
            const element = elements[i] as HTMLElement;

            let information: string = "";
            let informationType: string = "";

            switch (element.tagName) {
                case 'TEXTAREA':
                    const textAreaElement = element as HTMLTextAreaElement;
                    if (element.classList.contains('input-hyperlink-iframe')) {
                        information = textAreaElement.value;
                        informationType = 'Hyperlink'
                    } else {
                        information = textAreaElement.value;
                        informationType = 'Text';
                    }
                    break;
                case 'IMG':
                    const imageElement = element as HTMLImageElement;
                    information = imageElement.src.replace("data:image/png;base64,", "");
                    informationType = 'Image';
                    break;
                case 'VIDEO':
                    const videoElement = element as HTMLVideoElement;
                    information = videoElement.getAttribute("data-name")!;
                    informationType = 'Video';
                    break;
            }

            infoArray[i] = {
                id: infoArray[i].id,
                information: information,
                informationType: informationType
            }
        }
        currentStep.informationViewModel = infoArray;
    }

    if (isQuestionStep(currentStep)) {
        const questionDiv = divQuestion.children[0] as HTMLDivElement;
        const choicesDiv = divQuestion.children[1] as HTMLDivElement;
        let choicesArray: Choice[] = currentStep.questionViewModel.choices;
        let question: Question = currentStep.questionViewModel;

        for (let i = 0; i < choicesDiv.children.length; i++) {
            const input = choicesDiv.children[i].children[0] as HTMLInputElement;
            const select = choicesDiv.children[i].children[1] as HTMLSelectElement;

            let selectedOption = select.options[select.selectedIndex];
            if (selectedOption.value != "undefined") {
                choicesArray[i] = {
                    id: choicesArray[i].id,
                    text: input.value,
                    nextStepId: parseInt(selectedOption.value)
                };
            } else {
                choicesArray[i] = {id: choicesArray[i].id, text: input.value, nextStepId: undefined};
            }
        }

        const questionText = questionDiv.children[0] as HTMLTextAreaElement;

        question = {
            id: question.id,
            question: questionText.value,
            questionType: question.questionType,
            choices: choicesArray
        }

        currentStep.questionViewModel = question;
    }

    currentStepList[index] = currentStep;

    console.log(currentStepList)
}

async function showStepInContainer(step: Step) {
    currentStep = step;
    divInformation.innerHTML = "";
    divQuestion.innerHTML = "";

    toggleButtons();

    if (isInformationStep(currentStep)) {
        for (const info of currentStep.informationViewModel) {
            switch (info.informationType) {
                case 'Text': {
                    let text = document.createElement("textarea")
                    text.id = 'text-textarea'
                    text.value = info.information;
                    divInformation.appendChild(text);
                    break;
                }
                case 'Image': {
                    let image = document.createElement("img")
                    image.src = "data:image/png;base64," + info.information;
                    image.classList.add("col-m-12");
                    image.style.width = '600px';
                    image.style.height = '600px';
                    divInformation.appendChild(image);
                    let input = document.createElement("input");
                    input.type = "file";
                    input.accept = "image/*";
                    input.multiple = false;
                    input.onchange = async () => {
                        image.src = "data:image/png;base64," + await readFileAsBase64(input.files![0]);
                    }
                    divInformation.appendChild(input);
                    break;
                }
                case 'Video': {
                    let path = await downloadVideoFromBucket(info.information);
                    let video = document.createElement("video");
                    if (typeof path === "string") {
                        path = path.substring(1, path.length - 1);
                        video.src = path;
                    }
                    video.autoplay = true;
                    video.loop = true;
                    video.controls = false;
                    video.classList.add("h-75", "w-75");
                    let input = document.createElement("input");
                    input.type = "file";
                    input.accept = "video/*";
                    input.multiple = false;
                    input.onchange = async () => {
                        uploadVideoToBucket(input.files![0])
                            .then(name => {
                                video.setAttribute("data-name", name);
                                downloadVideoFromBucket(name).then(path => {
                                    if (typeof path === "string") {
                                        path = path.substring(1, path.length - 1);
                                        video.src = path;
                                    }
                                });
                            })
                    }
                    divInformation.appendChild(video);
                    divInformation.appendChild(input);
                    break;
                }
                case 'Hyperlink': {
                    let iframe = document.createElement("textarea");
                    iframe.value = info.information;
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
                    divInformation.appendChild(iframe);
                    break;
                }
            }
        }
    }

    if (isQuestionStep(currentStep)) {
        let questionContainer = document.createElement("div");
        questionContainer.id = "question-container"
        questionContainer.classList.add("text-start", "m-auto");

        const textArea = document.createElement("textarea");
        textArea.id = "question-textarea"
        textArea.value = currentStep.questionViewModel.question;

        questionContainer.append(textArea);
        divQuestion.appendChild(questionContainer);

        let choiceContainer = document.createElement('div');
        choiceContainer.id = "choices-container";

        switch (currentStep.questionViewModel.questionType) {
            case 'SingleChoiceQuestion':
                await showQuestionChoices(currentStep.questionViewModel.choices, choiceContainer)
                break;
            case 'MultipleChoiceQuestion':
                await showQuestionChoices(currentStep.questionViewModel.choices, choiceContainer)
                break;
            case 'RangeQuestion':
                await showQuestionChoices(currentStep.questionViewModel.choices, choiceContainer)
                break;
            default: break;
        }

        divQuestion.appendChild(choiceContainer);
    }
}

async function showQuestionChoices(choices: Choice[], choiceContainer: HTMLDivElement) {
    for (const element of choices) {
        let div = document.createElement("div");
        div.classList.add("text-start", "m-auto", "choice-container")

        const textArea = document.createElement("input");
        textArea.classList.add("choice-textarea")
        textArea.value = element.text;

        const select = document.createElement("select");
        select.classList.add("choice-select");
        await fillConditionalPoints(select, element.nextStepId);

        div.append(textArea);
        div.append(select);
        choiceContainer.appendChild(div);
    }
}

async function fillConditionalPoints(select: HTMLSelectElement, nextStepId?: number) {
    let option = document.createElement("option");
    option.value = "undefined";
    option.innerText = "Next step"
    option.selected = true;
    select.appendChild(option);

    for (const step of currentStepList) {
        let option = document.createElement("option");
        await GetStepId(step.stepNumber).then(id => option.value = id.toString());
        option.innerText = `Step ${step.stepNumber}`
        select.appendChild(option);
        if (parseInt(option.value) == nextStepId)
            option.selected = true;
    }
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
    let newStepNumber = currentStepList.length == 0 ? 1 : currentStepList[currentStepList.length - 1].stepNumber + 1
    if (infographic.checked) {
        AddStep(newStepNumber, "Information")
            .then(() => GetStepsFromFlow(flowId)
                .then(steps => updateStepList(steps)))
            .then(() => initializeCardLinks());
    } else if (singleQ.checked) {
        AddStep(newStepNumber, "Single Choice Question")
            .then(() => GetStepsFromFlow(flowId)
                .then(steps => updateStepList(steps)))
            .then(() => initializeCardLinks());
    } else if (multipleQ.checked) {
        AddStep(newStepNumber, "Multiple Choice Question")
            .then(() => GetStepsFromFlow(flowId)
                .then(steps => updateStepList(steps)))
            .then(() => initializeCardLinks());
    } else if (rangeQ.checked) {
        AddStep(newStepNumber, "Ranged Question")
            .then(() => GetStepsFromFlow(flowId)
                .then(steps => updateStepList(steps)))
            .then(() => initializeCardLinks());
    } else if (openQ.checked) {
        AddStep(newStepNumber, "Open Question")
            .then(() => GetStepsFromFlow(flowId)
                .then(steps => updateStepList(steps)))
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
    saveFlow();
}

btnConfirmViewFlow.onclick = () => {
    clearModal();
}

btnStepVisibility.onclick = () => {
    const index = currentStepList.findIndex(s => s.stepNumber === currentStep.stepNumber);

    if (btnStepVisibility.innerText == 'Disable Step') {
        currentStep.visible = false;
        currentStepList[index].visible = false;
    } else if (btnStepVisibility.innerText == 'Enable Step') {
        currentStep.visible = true;
        currentStepList[index].visible = true;
    }

    toggleButtons();
    showStepVisibility(currentStepList[index]);
}

function checkURLValidity(url: string): boolean {
    let urlRegex = /(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?\/[a-zA-Z0-9]{2,}|((https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?)|(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}(\.[a-zA-Z0-9]{2,})?/g;

    return urlRegex.test(url);
}
