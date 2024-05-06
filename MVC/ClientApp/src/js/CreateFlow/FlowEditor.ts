import {Step} from "../Flow/Step/StepObjects";
import {downloadVideoFromBucket} from "../StorageAPI";
import {Modal} from "bootstrap";

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
      .then(data => {
        ShowStepInContainer(data);
        currentViewingStep = data;
      })
      .then(() => toggleButtons())
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
      cardHeader.innerText = "Step " + step.stepNumber.toString() + "\n" + step.stepName;
      
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
  
  GetSteps(flowId)
      .then(() => initializeCardLinks())
      .then(() => toggleButtons());
  
});

btnAddChoice.addEventListener('click', () => {
  console.log("Add choice")
  let div = document.createElement("div");
  div.classList.add("text-start", "m-auto", "choice-container")

  const radioButton = document.createElement("input");
  radioButton.classList.add("btn-choice")
  radioButton.type = "radio";
  radioButton.name = "choices";
  radioButton.value = "";

  const textArea = document.createElement("input");
  textArea.classList.add("choice-textarea")

  div.append(radioButton);
  div.append(textArea);
  
  let choiceContainer = document.getElementById("choices-container") as HTMLDivElement;
  
  choiceContainer.appendChild(div);
});

btnAddText.addEventListener('click', () => {
  console.log("Add text")
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
  console.log("Saving flow...")
  if (currentViewingStep == undefined) { return; }
  if (currentViewingStep.informationViewModel != undefined) {
    const contentElements = partialInformationContainer.children; // Get all child elements of the content div

    // Initialize arrays to store content and information types
    const content: string[] = [];
    const informationTypes: string[] = [];

    // Loop through each child element of the content div
    for (let i = 0; i < contentElements.length; i++) {
      const element = contentElements[i] as HTMLElement; // Cast to HTMLElement

      // Determine the tag name and cast the element accordingly
      switch (element.tagName) {
        case 'P':
          const paragraphElement = element as HTMLParagraphElement;
          content.push(paragraphElement.textContent as string); // Add text content or empty string
          informationTypes.push('Text'); // Set information type to Text
          break;
        case 'IMG':
          const imageElement = element as HTMLImageElement;
          content.push(imageElement.src); // Add image source or empty string
          informationTypes.push('Image'); // Set information type to Image
          break;
        case 'VIDEO':
          const videoElement = element as HTMLVideoElement;
          content.push(videoElement.src); // Add video source or empty string
          informationTypes.push('Video'); // Set information type to Video
          break;
        case 'INPUT':
          const inputElement = element as HTMLInputElement;
          content.push(inputElement.value); // Add input value or empty string
          informationTypes.push('Text'); // Set information type to Text
          break;
        default:
          // Handle other element types if needed
          break;
      }
    }

    // Prepare data to send in the fetch request
    const data = {
      content,
      informationTypes
    };
    
    //TODO: Jana maakt dit verder af -> fetch hier.
  }
})

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
    let p = document.createElement("input");
    p.value = data.questionViewModel.question;
    p.classList.add("text-start", "m-auto", "mb-3", "question-header");
    partialQuestionContainer.appendChild(p);
    
    let choiceContainer = document.createElement('div');
    choiceContainer.id = "choices-container";
    switch (data.questionViewModel.questionType) {
      case "SingleChoiceQuestion":
        for (const element of data.questionViewModel.choices) {
          
          let div = document.createElement("div");
          div.classList.add("text-start", "m-auto", "choice-container")
          
          const radioButton = document.createElement("input");
          radioButton.classList.add("btn-choice")
          radioButton.type = "radio";
          radioButton.name = "choices";
          radioButton.value = element.text;

          const textArea = document.createElement("input");
          textArea.classList.add("choice-textarea")
          textArea.value = element.text;
          
          div.append(radioButton);
          div.append(textArea);
          choiceContainer.appendChild(div);
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
    partialQuestionContainer.appendChild(choiceContainer);
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