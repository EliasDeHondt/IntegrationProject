import {Step} from "./Step/StepObjects";
import {downloadVideoFromBucket} from "../StorageAPI";
import {Flow} from "./FlowObjects";
import {Modal} from "bootstrap";

const questionContainer = document.getElementById("questionContainer") as HTMLDivElement;
const informationContainer = document.getElementById("informationContainer") as HTMLDivElement;
const btnNextStep = document.getElementById("btnNextStep") as HTMLButtonElement;
const btnRestartFlow = document.getElementById("btnRestartFlow") as HTMLButtonElement;
const btnPauseFlow = document.getElementById("btnPauseFlow") as HTMLButtonElement;
const btnUnPauseFlow = document.getElementById("btnUnPauseFlow") as HTMLButtonElement;
const btnEmail = document.getElementById("btnEmail") as HTMLButtonElement;
const btnExitFlow = document.getElementById("butExitFlow") as HTMLButtonElement;
const modal = new Modal(document.getElementById("pausedFlowModal") as HTMLDivElement,{
    backdrop: 'static',
    keyboard: false
});
const btnShowFlows = document.getElementById("flowDropdownBtn") as HTMLButtonElement;
const ddFlows = document.getElementById("flowDropdown") as HTMLUListElement;
let currentStepNumber: number = 0;
let userAnswers: string[] = []; // Array to store user answers
let openUserAnswer: string = "";
let flowId = Number((document.getElementById("flowId") as HTMLSpanElement).innerText);
let themeId = Number((document.getElementById("theme") as HTMLSpanElement).innerText);
let stepTotal = Number((document.getElementById("stepTotal") as HTMLSpanElement).innerText);
let flowtype = (document.getElementById("flowtype") as HTMLSpanElement).innerText;
let prevFlowId = sessionStorage.getItem('prevFlowId');

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
        .then(data => ShowStep(data))
        .catch(error => console.error("Error:", error))
}

async function ShowStep(data: Step) {
    (document.getElementById("stepNr") as HTMLSpanElement).innerText = currentStepNumber.toString();
    informationContainer.innerHTML = "";
    questionContainer.innerHTML = "";
    if (data.informationViewModel != undefined) {
        switch (data.informationViewModel.informationType) {
            case "Text": {
                let p = document.createElement("p");
                p.innerText = data.informationViewModel.information;
                p.classList.add("text-center");
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

    if (data.questionViewModel != undefined) {
        let p = document.createElement("p");
        p.innerText = data.questionViewModel.question;
        p.classList.add("text-start");
        p.classList.add("m-auto");
        p.classList.add("mb-3");
        questionContainer.appendChild(p);
        switch (data.questionViewModel.questionType) {
            case "SingleChoiceQuestion":
                for (const element of data.questionViewModel.choices) {
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
                for (const element of data.questionViewModel.choices) {
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
                slider.max = String(data.questionViewModel.choices.length - 1);
                slider.step = String(1);

                userAnswers = [data.questionViewModel.choices[Number(slider.value)].text];

                div.appendChild(slider);

                let label = document.createElement("label");
                label.innerText = data.questionViewModel.choices[Number(slider.value)].text;
                div.appendChild(label);
                questionContainer.appendChild(div);
                questionContainer.appendChild(label);

                slider.addEventListener('input', function () {
                    // Update the label to reflect the current choice
                    userAnswers = [data.questionViewModel.choices[Number(slider.value)].text];
                    label.innerText = data.questionViewModel.choices[Number(slider.value)].text;
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
                console.log("This question type is not currently supported. (QuestionType: " + data.questionViewModel.questionType);
                break;
        }
    }
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

btnNextStep.onclick = async () => {
    if (userAnswers.length > 0 || openUserAnswer.length > 0) {
        await saveAnswerToDatabase(userAnswers, openUserAnswer, flowId, currentStepNumber);
        // Clear the userAnswers array for the next step
        userAnswers = [];
        openUserAnswer = "";
    }
    // Proceed to the next step
    if (flowtype.toUpperCase() == "CIRCULAR" && currentStepNumber >= stepTotal) {
        currentStepNumber = 0;
        GetNextStep(++currentStepNumber, flowId);
    } else {
        GetNextStep(++currentStepNumber, flowId);
    }

}

btnRestartFlow.onclick = () => {
    currentStepNumber = 0;
    GetNextStep(++currentStepNumber, flowId);
};

btnPauseFlow.onclick = () => {
    UpdateFlowState(String(flowId), "Paused");
    modal.show();
};

if (btnUnPauseFlow)
    btnUnPauseFlow.onclick = () => {
        UpdateFlowState(String(flowId), "Active");
        modal.hide();
    }

btnExitFlow.onclick = () => UpdateFlowState(String(flowId), "Inactive");

btnShowFlows.onclick = () => {
    fetch(`/api/SubThemes/` + themeId + `/Flows`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => ShowFlows(data))
        .catch(error => console.error("Error:", error))
};

function ShowFlows(flows: Flow[]) {
    ddFlows.innerHTML = "";
    flows.forEach(flow => {
        if (flow.id != flowId)
            ddFlows.innerHTML += `<li><a class="dropdown-item" href="/Flow/Step/${flow.id}">${flow.id}</a></li>`
        else
            ddFlows.innerHTML += `<li><a class="dropdown-item active" aria-current="true">${flow.id}</a></li>`
    });
}

export function UpdateFlowState(id: string, state: string) {
    fetch("/api/Flows/" + id + "/" + state, {
        method: "PUT"
    })
        .then(response => {
            if (response.ok) {
                console.log(`Flow ${prevFlowId} ${state}!`)
                return true;
            }
            return false;
        })
        .catch(error => console.error("Error:", error))
}


function UpdateCurrentFlowState() {
    console.log(prevFlowId);
    if (prevFlowId != null)
        UpdateFlowState(prevFlowId, 'Inactive');
    UpdateFlowState(String(flowId), 'Active');
    sessionStorage.setItem('prevFlowId', String(flowId));
    console.log(prevFlowId);
}

window.onload = () => UpdateCurrentFlowState();
