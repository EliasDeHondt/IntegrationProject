import {Step} from "../Flow/Step/StepObjects";
import {step} from "@tensorflow/tfjs";

const stepsList = document.getElementById('steps-list') as HTMLElement;


let flowId = "";

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

function UpdateStepList(steps: Step[]) {
  
  steps.sort((a, b) => a.stepNumber - b.stepNumber);
  
  const stepsList = document.getElementById("steps-list") as HTMLDivElement;
  
  if (steps.length > 0) {
    steps.forEach(step => {

      //Card container
      const stepCard = document.createElement('a');
      stepCard.classList.add("step-card", "btn");
      stepCard.dataset.stepId = step.id.toString();
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
  const flowId = parseInt(lastPart, 10);
  
  if (isNaN(flowId)) { console.error("The ID provided in the URL is not a number.")}
  
  GetSteps(flowId);
  
});