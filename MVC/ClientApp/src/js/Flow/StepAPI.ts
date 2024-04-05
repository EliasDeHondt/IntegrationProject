import { Step } from "./Step/StepObjects";
import { downloadVideoFromBucket } from "../StorageAPI";
import {Flow} from "./FlowObjects";

const questionContainer = document.getElementById("questionContainer") as HTMLDivElement;
const informationContainer = document.getElementById("informationContainer") as HTMLDivElement;
const btnNextStep = document.getElementById("btnNextStep") as HTMLButtonElement;
const btnRestartFlow = document.getElementById("btnRestartFlow") as HTMLButtonElement;
let currentStepNumber: number = 0;
let userAnswers: string[] = []; // Array to store user answers
let openUserAnswer: string = "";
let flowId = Number((document.getElementById("flowId") as HTMLSpanElement).innerText);
let themeId = Number((document.getElementById("theme") as HTMLSpanElement).innerText);
let steptotal = Number((document.getElementById("steptotal") as HTMLSpanElement).innerText);
let flowtype = (document.getElementById("flowtype") as HTMLSpanElement).innerText;
let inputEmail = (document.getElementById("inputEmail") as HTMLInputElement).value;


function SetRespondentEmail(){
    flowId
}
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
                p.classList.add("col-md-12");
                informationContainer.appendChild(p);
                break;
            }
            case "Image": {
                let img = document.createElement("img");
                img.src = "data:image/png;base64," + data.informationViewModel.information;
                img.classList.add("col-m-12","w-100","h-100");
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
                video.classList.add("h-100","w-100");
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
                for (let i = 0; i < data.questionViewModel.choices.length; i++) {
                    let choice = document.createElement("input");
                    let label = document.createElement("label");
                    let div = document.createElement("div");
                    div.classList.add("text-start","m-auto")
                    choice.type = 'radio';
                    choice.name = 'choice';
                    choice.value = data.questionViewModel.choices[i].text;
                    label.appendChild(choice);
                    label.append(data.questionViewModel.choices[i].text);
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
                for (let i = 0; i < data.questionViewModel.choices.length; i++) {
                    let choice = document.createElement("input");
                    let label = document.createElement("label");
                    let div = document.createElement("div");
                    div.classList.add("text-start","m-auto")
                    choice.type = 'checkbox';
                    choice.name = 'choice';
                    choice.value = data.questionViewModel.choices[i].text;
                    label.appendChild(choice);
                    label.append(data.questionViewModel.choices[i].text);
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
            case "RangeQuestion":
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
            case "OpenQuestion":
                let textInput = document.createElement("textarea");
                let opendiv = document.createElement("div");
                opendiv.classList.add("m-auto");
                textInput.classList.add("w-100");
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
                opendiv.append(textInput);
                questionContainer.appendChild(opendiv);
                break;
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
        // Save user answers to the database
        console.log("Saving data...");
        for (let i = 0; i < userAnswers.length; i++) {
            console.log(userAnswers[i]);
        }
        await saveAnswerToDatabase(userAnswers, openUserAnswer, flowId, currentStepNumber);
        // Clear the userAnswers array for the next step
        userAnswers = [];
        openUserAnswer = "";
    }
    // Proceed to the next step
    if (flowtype == "CIRCULAR" && currentStepNumber >= steptotal) {
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