import {Step} from "../Flow/Step/StepObjects";
import {downloadVideoFromBucket} from "../StorageAPI";
import {Modal} from "bootstrap";

const stepsList = document.getElementById('steps-list') as HTMLElement;
const btnAddStep = document.getElementById('btn-add-step') as HTMLButtonElement;

let currentStepList: Step[];

let flowId: number;

async function GetSteps(flowId : number) {
  console.log("Fetching steps...");
  await fetch("/EditFlows/GetSteps/" + flowId, {
    method: "GET",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    }
  })
      .then(response => response.json())
      .then(data => UpdateStepList(Object.values(data)))
      .catch(error => console.error("Error:", error))
}

function GetStepById(stepNumber: number, flowId: number) {
  fetch("/api/Steps/GetNextStep/" + flowId + "/" + stepNumber, {
    method: "GET",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    }
  })
      .then(response => response.json())
      .then(data => ShowStepInContainer(data))
      .catch(error => console.error("Error:", error))
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

function UpdateStepList(steps: Step[]) {
  
  steps.sort((a, b) => a.stepNumber - b.stepNumber);
  
  currentStepList = steps;
  
  const stepsList = document.getElementById("steps-list") as HTMLDivElement;
  
  stepsList.innerHTML = "";
  
  if (steps.length > 0) {
    steps.forEach(step => {

      //Card container
      const stepCard = document.createElement('a');
      stepCard.classList.add("step-card", "btn");
      stepCard.dataset.stepId = step.id.toString();
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
  
  if (isNaN(flowId)) { console.error("The ID provided in the URL is not a number.")}
  
  GetSteps(flowId).then(() => { initializeCardLinks(); });
  
});

function initializeCardLinks() {
  let stepCards = document.querySelectorAll('.step-card') as NodeListOf<HTMLAnchorElement>;

  stepCards.forEach(stepCard => {
    stepCard.addEventListener('click', () => {
      const stepNumber = stepCard.dataset.stepNumber;

      if (stepNumber) {
        GetStepById(parseInt(stepNumber), flowId);        
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
  if (data.informationViewModel != undefined) {
    switch (data.informationViewModel.informationType) {
      case "Text": {
        let p = document.createElement("p");
        p.innerText = data.informationViewModel.information;
        p.classList.add("text-center");
        p.classList.add("col-md-12");
        partialInformationContainer.appendChild(p);
        break;
      }
      case "Image": {
        let img = document.createElement("img");
        img.src = "data:image/png;base64," + data.informationViewModel.information;
        img.classList.add("col-m-12", "w-100", "h-100");
        partialInformationContainer.appendChild(img);
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
        video.classList.add("h-100", "w-100");
        partialInformationContainer.appendChild(video);
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
    partialQuestionContainer.appendChild(p);
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
          partialQuestionContainer.appendChild(div);
        }
        break;
      case "MultipleChoiceQuestion":
        console.log(data);
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
          partialQuestionContainer.appendChild(div);
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

        div.appendChild(slider);

        let label = document.createElement("label");
        label.innerText = data.questionViewModel.choices[Number(slider.value)].text;
        div.appendChild(label);
        partialQuestionContainer.appendChild(div);
        partialQuestionContainer.appendChild(label);
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
        textInput.maxLength = 650;
        textInput.placeholder = "Your answer here... (Max 650 characters)"

        // Event listener that ensures the 650 character limit.
        textInput.addEventListener('input', function () {
          let currentLength = textInput.value.length;
          if (currentLength > 650) {
            textInput.value = textInput.value.substring(0, 650);
          }
        });
        openDiv.append(textInput);
        partialQuestionContainer.appendChild(openDiv);
        break;
      }
      default:
        console.log("This question type is not currently supported. (QuestionType: " + data.questionViewModel.questionType);
        break;
    }
  }
}



// btnAddStep.addEventListener('click', () => {
//  
//   let newStepNumber = currentStepList[currentStepList.length - 1].stepNumber + 1
//
//   //Right now only makes information steps.
//   AddStep(newStepNumber,"Information")
//       .then(() => GetSteps(flowId))
//       .then(() => initializeCardLinks());
// });
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