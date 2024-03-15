import {Step} from "./Step/StepObjects";

let nextStepButton = document.getElementById("butNextStep") as HTMLButtonElement;
let informationContainer = document.getElementById("informationContainer") as HTMLDivElement;
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
        informationContainer.appendChild(p);
        switch(data.questionViewModel.questionType) {
            case "SingleChoiceQuestion":
                console.log("yo " + data.questionViewModel.choices + " " + data.questionViewModel.choices.length)
                for (var i = 0; i < data.questionViewModel.choices.length; i++) {
                    console.log(i + " " + data.questionViewModel.choices[i].text)
                    let test = document.createElement("p")
                    test.innerText = data.questionViewModel.choices[i].text;
                    informationContainer.appendChild(test);
                    /*let radiobutton = document.createElement("button")
                    radiobutton.id = "choices";
                    radiobutton.innerText = data.questionViewModel.choices[i].text;
                    informationContainer.appendChild(radiobutton);*/
                }
                break;
            case "MultipleChoiceQuestion":
                break;
            case "RangeQuestion":
                break;
            case "OpenQuestion":
                break;
            default:
                console.log("This question type is not currently supported. (QuestionType: " + data.questionViewModel.questionType);
                break;
        }
    }
}

nextStepButton.onclick = () => GetNextStep(++currentStepNumber, 1)