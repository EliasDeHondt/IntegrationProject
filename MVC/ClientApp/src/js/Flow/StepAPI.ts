import {Step} from "./Step/StepObjects";

let nextStepButton = document.getElementById("butNextStep") as HTMLButtonElement;
let informationContainer = document.getElementById("informationContainer") as HTMLDivElement;
let questionContainer = document.getElementById("questionContainer") as HTMLDivElement;
let currentStepNumber: number = 0;
let flowId: number; // TODO: voor later multiple flows

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
    questionContainer.innerHTML = "";
    if (data.informationViewModel != undefined) {
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
    
    if (data.questionViewModel != undefined) {
        let p = document.createElement("p");
        p.innerText = data.questionViewModel.question;
        questionContainer.appendChild(p);
        switch(data.questionViewModel.questionType) {
            case "SingleChoiceQuestion" :
                for (let i = 0; i < data.questionViewModel.choices.length; i++) {
                    let choice = document.createElement("input");
                    let label = document.createElement("label");
                    choice.type = 'radio';
                    choice.name = 'choice';
                    choice.value = data.questionViewModel.choices[i].text;
                    label.appendChild(choice);
                    label.append(data.questionViewModel.choices[i].text);
                    label.style.display = 'block';
                    questionContainer.appendChild(label);
                }
                break;
            case "MultipleChoiceQuestion":
                for (let i = 0; i < data.questionViewModel.choices.length; i++) {
                    let choice = document.createElement("input");
                    let label = document.createElement("label");
                    choice.type = 'checkbox';
                    choice.name = 'choice';
                    choice.value = data.questionViewModel.choices[i].text; 
                    label.appendChild(choice);
                    label.append(data.questionViewModel.choices[i].text);
                    label.style.display = 'block';
                    questionContainer.appendChild(label);
                }
                break;
            case "RangeQuestion": //TODO: nog bespreken hoe we dit doen, atm hetzelfde als singlechoice
                for (let i = 0; i < data.questionViewModel.choices.length; i++) {
                    let choice = document.createElement("input");
                    let label = document.createElement("label");
                    choice.type = 'radio';
                    choice.name = 'choice';
                    choice.value = data.questionViewModel.choices[i].text;
                    label.appendChild(choice);
                    label.append(data.questionViewModel.choices[i].text);
                    label.style.display = 'block';
                    questionContainer.appendChild(label);
                }
                break;
            case "OpenQuestion":
                let textInput = document.createElement("input");
                textInput.type = 'text';
                textInput.name = 'answer'
                questionContainer.appendChild(textInput);
                break;
            default:
                console.log("This question type is not currently supported. (QuestionType: " + data.questionViewModel.questionType);
                break;
        }
    }
}

nextStepButton.onclick = () => GetNextStep(++currentStepNumber, 1)