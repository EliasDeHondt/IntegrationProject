import {Information, Question, Step} from "./Step/StepObjects";
import {downloadVideoFromBucket} from "../StorageAPI";
import * as phyAPI from "../Webcam/WebCamDetection";
import {detectionCanvas, drawChoiceBoundaries, getResult} from "../Webcam/WebCamDetection";
import {delay} from "../Util";

const questionContainer = document.getElementById("questionContainer") as HTMLDivElement;
const informationContainer = document.getElementById("informationContainer") as HTMLDivElement;
const btnNextStep = document.getElementById("btnNextStep") as HTMLButtonElement;
const btnRestartFlow = document.getElementById("btnRestartFlow") as HTMLButtonElement;
const btnEmail = document.getElementById("btnEmail") as HTMLButtonElement;
let currentStepNumber: number = 0;
let userAnswers: string[] = []; // Array to store user answers
let openUserAnswer: string = "";
let flowId = Number((document.getElementById("flowId") as HTMLSpanElement).innerText);
let stepTotal = Number((document.getElementById("stepTotal") as HTMLSpanElement).innerText);
let flowtype = sessionStorage.getItem("flowType")!;
let sessionCode = sessionStorage.getItem("connectionCode")!;
let timerInterval
let nextStepInterval;
let time: number = 29;
let choices: string[] = [];

hideDigitalElements();

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
document.addEventListener("DOMContentLoaded", function () {
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

function GetNextStep(stepNumber: number, flowId: number) {
    fetch("/api/Steps/GetNextStep/" + flowId + "/" + stepNumber, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(async (data) => {
            if(flowtype.toUpperCase() == "PHYSICAL"){
                await showPhysicalStep(data)
            } else {
                await ShowStep(data)
            }
        } )
        .catch(error => console.error("Error:", error))
}


async function showPhysicalStep(data: Step){
    informationContainer.innerHTML = "";
    questionContainer.innerHTML = "";
    if(data.informationViewModel != undefined && data.questionViewModel != undefined) nextStep(false).then(() => {
        return
    });
    await showInformationStep(data.informationViewModel);
    showPhysicalQuestionStep(data.questionViewModel);
}

async function showInformationStep(data: Information){
    if (data != undefined) {
        const webcam = document.getElementById("webcamDiv") as HTMLDivElement;
        webcam.classList.add("visually-hidden");
        switch (data.informationType) {
            case "Text": {
                let p = document.createElement("p");
                p.innerText = data.information;
                p.classList.add("text-center");
                informationContainer.appendChild(p);
                break;
            }
            case "Image": {
                let img = document.createElement("img");
                img.src = "data:image/png;base64," + data.information;
                informationContainer.appendChild(img);
                break;
            }
            case "Video": {
                let path = await downloadVideoFromBucket(data.information);
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
}

function showPhysicalQuestionStep(data: Question){
    choices = [];
    if (data != undefined) {
        const webcam = document.getElementById("webcamDiv") as HTMLDivElement;
        webcam.classList.remove("visually-hidden");
        switch(data.questionType){
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

function createQuestion(data: Question){
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
    rowDiv.classList.add("w-50");
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

function showQuestionStep(data: Question){
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
                });
                break;
            }
            case "OpenQuestion": {
                let textInput = document.createElement("textarea");
                let openDiv = document.createElement("div");
                openDiv.classList.add("m-auto");
                textInput.name = 'answer';
                textInput.rows = 8;
                textInput.cols = 75;
                textInput.maxLength = 650;
                textInput.placeholder = "Your answer here... (Max 650 characters)"

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
    await showInformationStep(data.informationViewModel);
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

async function hideDigitalElements(){
    if(flowtype.toUpperCase() == "PHYSICAL") {
        const digitalElements = document.getElementsByClassName("digital-element");
        for (let i = 0; i < digitalElements.length; i++) {
            digitalElements[i].classList.add("visually-hidden");
        }
        const webcam = document.getElementById("webcamDiv") as HTMLDivElement;
        const topLeft = document.getElementById("topLeft") as HTMLDivElement;
        const themeDiv = document.getElementById("themeDiv") as HTMLDivElement;
        const themeDivInner = themeDiv.innerHTML
        topLeft.style.height = "120px";
        topLeft.innerHTML = `<h1>${themeDivInner}</h1><p>Facilitator code: ${sessionCode}</p>`
        webcam.classList.remove("visually-hidden");

        const centerDiv = document.getElementById("kioskCenter") as HTMLDivElement;
        centerDiv.style.top = "58%";
        const centerContainerDiv = document.getElementById("kioskContainerCenter") as HTMLDivElement;
        centerContainerDiv.style.height = "800px";
        
        const timer = document.getElementById("timer") as HTMLDivElement;
        timer.innerText = "Loading physical setup...";
        
        let model = await phyAPI.loadModel();
        phyAPI.startPhysical(model).then(async () => {
            await delay(1000);
            GetNextStep(++currentStepNumber, flowId);
            startTimers();
        });
    } else {
        btnNextStep.onclick = async () => {
            // Proceed to the next step
            await nextStep();
        }
    }
}

async function nextStep(save: boolean = true){
    if(save) {
        if (flowtype.toUpperCase() == "PHYSICAL") {
            let answers: number[] = getResult();
            if(choices.length > 0){
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
        GetNextStep(++currentStepNumber, flowId);
    } else {
        GetNextStep(++currentStepNumber, flowId);
    }
    time = 30;
}

function updateClock(){
    const timer = document.getElementById("timer") as HTMLDivElement;
    time -= 1
    timer.innerText = time.toString();
}

function startTimers(){
    timerInterval = setInterval(updateClock, 1000);
    nextStepInterval = setInterval(nextStep, 30000);
}



btnRestartFlow.onclick = () => {
    currentStepNumber = 0;
    GetNextStep(++currentStepNumber, flowId);
};