import {Information, isInformationStep, isQuestionStep, Question, Step} from "./Step/StepObjects";
import {downloadVideoFromBucket} from "../StorageAPI";
import * as phyAPI from "../Webcam/WebCamDetection";
import {detectionCanvas, drawChoiceBoundaries, getResult} from "../Webcam/WebCamDetection";
import {delay} from "../Util";
import {Timer} from "../Util/Timer";
import {Modal} from "bootstrap";
import * as kiosk from "../Kiosk/Kiosk"
import {HubConnectionState} from "@microsoft/signalr";

const questionContainer = document.getElementById("questionContainer") as HTMLDivElement;
const informationContainer = document.getElementById("informationContainer") as HTMLDivElement;
const btnNextStep = document.getElementById("btnNextStep") as HTMLButtonElement;
const btnRestartFlow = document.getElementById("btnRestartFlow") as HTMLButtonElement;
const btnPauseFlow = document.getElementById("btnPauseFlow") as HTMLButtonElement;
const btnUnPauseFlow = document.getElementById("btnUnPauseFlow") as HTMLButtonElement;
const btnEmail = document.getElementById("btnEmail") as HTMLButtonElement;
const btnExitFlow = document.getElementById("butExitFlow") as HTMLButtonElement;
const modal = new Modal(document.getElementById("pausedFlowModal") as HTMLDivElement, {
    backdrop: 'static',
    keyboard: false
});
const btnShowFlows = document.getElementById("flowDropdownBtn") as HTMLButtonElement;
const ddFlows = document.getElementById("flowDropdown") as HTMLUListElement;
let currentStepNumber: number = 0;
let userAnswers: string[] = []; // Array to store user answers
let openUserAnswer: string = "";
let flowId: number = Number((document.getElementById("flowId") as HTMLSpanElement).innerText);
let stepTotal = Number((document.getElementById("stepTotal") as HTMLSpanElement).innerText);
let flowtype = sessionStorage.getItem("flowType")!;
let sessionCode = sessionStorage.getItem("connectionCode")!;

export let stepTimer = new Timer(nextStep, 30000);
export let clockTimer = new Timer(updateClock, 1000);

let time: number = 29;
let choices: string[] = [];

hideDigitalElements();
let prevFlowId = sessionStorage.getItem('prevFlowId');
let currentState: string = "";
let conditionalAnswer: number = 0;

//email checken
function CheckEmail(inputEmail: string): boolean {
    const emailRegex: RegExp = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    let p = document.getElementById("errorMsg") as HTMLElement;
    if (emailRegex.test(inputEmail)) {
        p.innerHTML = "Email submitted!";
        p.style.color = "blue";
        return true;
    } else {

        //let p = document.getElementById("errorMsg") as HTMLElement;
        p.innerHTML = "Not an Email.";
        p.style.color = "red";
        // }
        return false;
    }
}

//email doorsturen
async function SetRespondentEmail(flowId: number, inputEmail: string) {
    try {
        const response = await fetch("/api/Flows/SetRespondentEmail/" + flowId + "/" + inputEmail, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                flowId: flowId,
                email: inputEmail
            })
        });
        if (response.ok) {
            console.log("Email saved successfully.");
        } else {
            console.error("Failed to save Email.");
        }
    } catch (error) {
        console.error("Error:", error);
    }
}

//button submit email 
document.addEventListener("DOMContentLoaded", async function () {
    await kiosk.connection.start().then(() => {
        kiosk.connection.invoke("JoinConnection", kiosk.code).then(() => {
            kiosk.connection.invoke("SendCurrentStep", kiosk.code, currentStepNumber)
        })
    });

    const emailInput = document.getElementById("inputEmail");

    btnEmail.onclick = function () {
        console.log("click");
        // @ts-ignore
        const inputEmail = emailInput.value.trim();
        const inputElement = emailInput as HTMLInputElement;

        if (CheckEmail(inputEmail)) {
            console.log("Correct Email.");
            if (inputEmail !== "") {
                SetRespondentEmail(flowId, inputEmail);

                if (inputElement !== null) {
                    inputElement.value = "";
                    console.log("Reset value.");
                } else {
                    console.log("No value to reset.");
                }
            }
        }
    };
});

async function GetNextStep(stepNumber: number, flowId: number): Promise<Step> {
    return fetch("/api/Steps/GetNextStep/" + flowId + "/" + stepNumber, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(async (data): Promise<Step> => {
            await kiosk.connection.invoke("SendCurrentStep", kiosk.code, stepNumber);
            if (!data.visible) {
                await GetNextStep(++currentStepNumber, flowId)
            } else {
                if (flowtype.toUpperCase() == "PHYSICAL") {
                    await showPhysicalStep(data);
                } else {
                    await ShowStep(data);
                }
            }

            return data;
        })
        .catch(error => {
            console.error("Error:", error);
            throw error;
        });
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


async function showPhysicalStep(data: Step) {
    informationContainer.innerHTML = "";
    questionContainer.innerHTML = "";
    if (isInformationStep(data) && isQuestionStep(data)) nextStep(false).then(() => {
        return
    });
    if (isInformationStep(data))
        await showInformationStep(data.informationViewModel);
    if (isQuestionStep(data))
        showPhysicalQuestionStep(data.questionViewModel);
}

async function showInformationStep(data: Information[]) {
    if (data != undefined) {
        const webcam = document.getElementById("webcamDiv") as HTMLDivElement;
        webcam.classList.add("visually-hidden");
        for (const infoStep of data) {
            switch (infoStep.informationType) {
                case "Text": {
                    let p = document.createElement("p");
                    p.innerText = infoStep.information;
                    p.classList.add("text-center");
                    informationContainer.appendChild(p);
                    break;
                }
                case "Image": {
                    let img = document.createElement("img");
                    img.src = "data:image/png;base64," + infoStep.information;
                    informationContainer.appendChild(img);
                    break;
                }
                case "Video": {
                    let path = await downloadVideoFromBucket(infoStep.information);
                    let video = document.createElement("video");
                    if (typeof path === "string") {
                        path = path.substring(1, path.length - 1);
                        video.src = path;

                    }
                    break;
                }
                case "Hyperlink": {
                    let url = infoStep.information;
                    let iframe = document.createElement("iframe");
                    iframe.src = url;
                    iframe.classList.add("hyperlink-iframe");
                    informationContainer.appendChild(iframe);
                    break;
                }
            }
        }
    }
}

function showPhysicalQuestionStep(data: Question) {
    choices = [];
    if (data != undefined) {
        const webcam = document.getElementById("webcamDiv") as HTMLDivElement;
        webcam.classList.remove("visually-hidden");
        switch (data.questionType) {
            case "RangeQuestion":
                createQuestion(data)
                break;
            case "SingleChoiceQuestion":
                createQuestion(data)
                break;
            default:
                nextStep(false).then(() => {
                    return
                })
        }
    }
}

function createQuestion(data: Question) {
    let p = document.createElement("p");
    p.innerText = data.question;
    p.classList.add("text-start");
    p.classList.add("m-auto");
    p.classList.add("mb-3");
    p.style.fontSize = "36px";
    questionContainer.appendChild(p);
    drawChoiceBoundaries(data.choices.length, detectionCanvas.width, detectionCanvas.height);
    let rowDiv = document.createElement("div");
    rowDiv.classList.add("row");
    rowDiv.classList.add("m-auto");
    rowDiv.classList.add("w-100");
    for (const element of data.choices) {
        let colDiv = document.createElement("div");
        colDiv.classList.add("col");

        let choice = document.createElement("p");
        choice.innerText = element.text;
        choice.style.fontSize = "24px";
        choices.push(element.text);
        colDiv.appendChild(choice);
        rowDiv.appendChild(colDiv);
    }
    questionContainer.appendChild(rowDiv);
}

function showQuestionStep(data: Question) {
    if (data != undefined) {

        let p = document.createElement("p");
        p.innerText = data.question;
        p.classList.add("text-start");
        p.classList.add("m-auto");
        p.classList.add("mb-3");
        questionContainer.appendChild(p);
        switch (data.questionType) {
            case "SingleChoiceQuestion":
                for (const element of data.choices) {
                    let choice = document.createElement("input");
                    let label = document.createElement("label");
                    let div = document.createElement("div");
                    div.classList.add("text-start", "m-auto")
                    choice.type = 'radio';
                    choice.name = 'choice';
                    choice.value = element.text;
                    label.appendChild(choice);
                    label.append(element.text);
                    label.style.display = 'block';
                    div.append(label);
                    questionContainer.appendChild(div);
                    // Add event listener to capture user input
                    choice.addEventListener('change', function () {
                        userAnswers = [choice.value];
                        if (element.nextStepId != undefined)
                            conditionalAnswer = element.nextStepId;
                    });
                }
                break;
            case "MultipleChoiceQuestion":
                for (const element of data.choices) {
                    let choice = document.createElement("input");
                    let label = document.createElement("label");
                    let div = document.createElement("div");
                    div.classList.add("text-start", "m-auto")
                    choice.type = 'checkbox';
                    choice.name = 'choice';
                    choice.value = element.text;
                    label.appendChild(choice);
                    label.append(element.text);
                    label.style.display = 'block';
                    div.appendChild(label);
                    questionContainer.appendChild(div);
                    // Add event listener to capture user input
                    choice.addEventListener('change', function () {
                        if (choice.checked) {
                            // Add selected choice to userAnswers array
                            userAnswers.push(choice.value);
                            if (element.nextStepId != undefined)
                                conditionalAnswer = element.nextStepId;
                        } else {
                            // Remove deselected choice from userAnswers array
                            const index = userAnswers.indexOf(choice.value);
                            if (index !== -1) {
                                userAnswers.splice(index, 1);
                            }
                        }
                    });
                }
                break;
            case "RangeQuestion": {
                let slider = document.createElement("input");
                let div = document.createElement("div");
                div.classList.add("m-auto");
                slider.type = 'range';
                slider.min = String(0);
                slider.max = String(data.choices.length - 1);
                slider.step = String(1);

                userAnswers = [data.choices[Number(slider.value)].text];

                div.appendChild(slider);

                let label = document.createElement("label");
                label.innerText = data.choices[Number(slider.value)].text;
                div.appendChild(label);
                questionContainer.appendChild(div);
                questionContainer.appendChild(label);

                slider.addEventListener('input', function () {
                    // Update the label to reflect the current choice
                    userAnswers = [data.choices[Number(slider.value)].text];
                    label.innerText = data.choices[Number(slider.value)].text;
                    const nextStepId = data.choices[Number(slider.value)].nextStepId;
                    if (nextStepId !== undefined) {
                        conditionalAnswer = nextStepId;
                    }
                });
                break;
            }
            case "OpenQuestion": {
                let textInput = document.createElement("textarea");
                let openDiv = document.createElement("div");
                openDiv.classList.add("m-auto");
                textInput.classList.add("w-100");
                textInput.name = 'answer';
                textInput.rows = 8;
                textInput.cols = 75;
                textInput.maxLength = 300;
                textInput.placeholder = "Your answer here... (Max 300 characters)"

                // Event listener that ensures the 650 character limit.
                textInput.addEventListener('input', function () {
                    let currentLength = textInput.value.length;
                    if (currentLength > 650) {
                        textInput.value = textInput.value.substring(0, 650);
                    }
                    // Capture user input
                    openUserAnswer = textInput.value;
                });
                openDiv.append(textInput);
                questionContainer.appendChild(openDiv);
                break;
            }
            default:
                console.log("This question type is not currently supported. (QuestionType: " + data.questionType);
                break;
        }
    }
}

async function ShowStep(data: Step) {
    (document.getElementById("stepNr") as HTMLSpanElement).innerText = currentStepNumber.toString();
    informationContainer.innerHTML = "";
    questionContainer.innerHTML = "";
    if (isInformationStep(data))
        await showInformationStep(data.informationViewModel);
    if (isQuestionStep(data))
        showQuestionStep(data.questionViewModel);
}

async function saveAnswerToDatabase(answers: string[], openAnswer: string, flowId: number, stepNumber: number): Promise<void> {
    try {
        const response = await fetch("/api/answers/addanswer/" + flowId + '/' + stepNumber, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                answers: answers,
                answerText: openAnswer
            })
        });
        if (response.ok) {
            console.log("Answers saved successfully.");
        } else {
            console.error("Failed to save answers.");
        }
    } catch (error) {
        console.error("Error:", error);
    }
}

async function hideDigitalElements() {
    if (flowtype.toUpperCase() == "PHYSICAL") {
        const digitalElements = document.getElementsByClassName("digital-element");
        for (let i = 0; i < digitalElements.length; i++) {
            digitalElements[i].classList.add("visually-hidden");
        }
        const webcam = document.getElementById("webcamDiv") as HTMLDivElement;
        const topLeft = document.getElementById("topLeft") as HTMLDivElement;
        const themeDiv = document.getElementById("themeDiv") as HTMLDivElement;
        const themeDivInner = themeDiv.innerHTML
        topLeft.style.height = "120px";
        topLeft.innerHTML = topLeft.innerHTML.concat(`<h1>${themeDivInner}</h1><p>Facilitator code: ${sessionCode}</p>`)
        webcam.classList.remove("visually-hidden");

        const centerDiv = document.getElementById("kioskCenter") as HTMLDivElement;
        centerDiv.style.top = "58%";
        const centerContainerDiv = document.getElementById("kioskContainerCenter") as HTMLDivElement;
        centerContainerDiv.style.height = "800px";

        const timer = document.getElementById("timer") as HTMLDivElement;
        timer.classList.remove("visually-hidden");
        timer.innerText = "Loading physical setup...";

        let model = await phyAPI.loadModel();
        phyAPI.startPhysical(model).then(async () => {
            await delay(2500);
            await GetNextStep(++currentStepNumber, flowId);
            startTimers();
        });
    } else {
        btnNextStep.onclick = async () => {
            // Proceed to the next step
            await nextStep();
        }
    }
}

async function nextStep(save: boolean = true) {
    if (save) {
        if (flowtype.toUpperCase() == "PHYSICAL") {
            let answers: number[] = getResult();
            if (choices.length > 0) {
                answers.forEach(answer => {
                    userAnswers.push(choices[answer]);
                })
            }
            if (userAnswers.length > 0) {
                await saveAnswerToDatabase(userAnswers, openUserAnswer, flowId, currentStepNumber).then(() => {
                    userAnswers = [];
                });
            }
        } else {
            if (userAnswers.length > 0 || openUserAnswer.length > 0) {
                await saveAnswerToDatabase(userAnswers, openUserAnswer, flowId, currentStepNumber);
                // Clear the userAnswers array for the next step
                userAnswers = [];
                openUserAnswer = "";
            }
        }
    }
    if ((flowtype.toUpperCase() == "CIRCULAR" || flowtype.toUpperCase() == "PHYSICAL") && currentStepNumber >= stepTotal) {
        currentStepNumber = 0;
        await GetNextStep(++currentStepNumber, flowId);
    } else {
        if (conditionalAnswer > 0) {
            await GetConditionalNextStep(conditionalAnswer).then(step => {
                conditionalAnswer = 0;
                currentStepNumber = step.stepNumber
                GetNextStep(currentStepNumber, flowId);
            })
        } else {
            await GetNextStep(++currentStepNumber, flowId);
        }
    }
    time = 30;
}

function updateClock() {
    const timer = document.getElementById("timer") as HTMLDivElement;
    time -= 1
    timer.innerText = time.toString();
}

function startTimers() {
    clockTimer.start();
    stepTimer.start();
}


btnRestartFlow.onclick = async () => {
    currentStepNumber = 0;
    await GetNextStep(++currentStepNumber, flowId);
};