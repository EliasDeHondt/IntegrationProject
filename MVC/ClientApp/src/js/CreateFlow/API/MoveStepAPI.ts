import {Step} from "../../Flow/Step/StepObjects";


export function moveStepToLeft(step: Step){
    step.stepNumber--;
    // const index = currentStepList.indexOf(step);
    // if (index > 0) {
    //     // Swap steps
    //     [currentStepList[index], currentStepList[index - 1]] = [currentStepList[index - 1], currentStepList[index]];
    //     // Re-render the steps list
    //     renderStepsList();
    // }
}
export function moveStepToRight(step: Step){
    step.stepNumber++;
}

// export function moveStepToLeft(step: Step, currentStepList: string | any[] | undefined,stepsList: HTMLDivElement | undefined) {
//     // @ts-ignore
//     const index = currentStepList.indexOf(step);
//     if (index > 0) {
//         if (currentStepList != null) {
//             // @ts-ignore
//             [currentStepList[index - 1], currentStepList[index]] = [currentStepList[index], currentStepList[index - 1]];
//         }
//         renderSteps(stepsList,currentStepList);;
//     }
// }
//
// export function moveStepToRight(step: string | Step, currentStepList: string | any[] | undefined,stepsList: HTMLDivElement | undefined) {
//     // @ts-ignore
//     const index = currentStepList.indexOf(step);
//     // @ts-ignore
//     if (index < currentStepList.length - 1) {
//         if (currentStepList != null) {
//             // @ts-ignore
//             [currentStepList[index + 1], currentStepList[index]] = [currentStepList[index], currentStepList[index + 1]];
//         }
//         renderSteps(stepsList,currentStepList);
//     }
// }
//
// function renderSteps(stepsList: HTMLDivElement | undefined, currentStepList: string | any[] | undefined) {
//     // @ts-ignore
//     stepsList.innerHTML = "";
//     // @ts-ignore
//     currentStepList.forEach(step => {
//         const stepCard = document.createElement('a');
//         stepCard.classList.add("step-card", "justify-content-center", "align-items-center");
//         stepCard.dataset.stepNumber = step.stepNumber.toString();
//
//         const cardHeader = document.createElement('h2');
//         cardHeader.classList.add("step-card-header");
//         cardHeader.innerText = "Step " + step.stepNumber.toString();
//
//         if (step.questionViewModel)
//             cardHeader.innerText += "\n" + step.questionViewModel.questionType.toString();
//
//         if (!step.visible)
//             stepCard.classList.add('step-card-hidden');
//
//         stepCard.appendChild(cardHeader);
//
//         const buttons = document.createElement('div');
//         buttons.classList.add("step-btns", "justify-content-center", "align-items-center");
//
//         const leftArrowButton = document.createElement('button');
//         const leftArrowIcon = document.createElement('i');
//         leftArrowIcon.classList.add('bi', 'bi-caret-left-fill');
//         leftArrowButton.classList.add('arrow-button', 'left-arrow');
//         leftArrowButton.addEventListener('click', () => {
//             moveStepToLeft(step,currentStepList,stepsList);
//         });
//         const rightArrowButton = document.createElement('button');
//         const rightArrowIcon = document.createElement('i');
//         rightArrowIcon.classList.add('bi', 'bi-caret-right-fill');
//         rightArrowButton.classList.add('arrow-button', 'right-arrow');
//         rightArrowButton.addEventListener('click', () => {
//             moveStepToRight(step,currentStepList,stepsList);
//         });
//         leftArrowButton.appendChild(leftArrowIcon);
//         rightArrowButton.appendChild(rightArrowIcon);
//         buttons.appendChild(leftArrowButton);
//         buttons.appendChild(rightArrowButton);
//
//         stepCard.appendChild(buttons);
//
//         // @ts-ignore
//         stepsList.appendChild(stepCard);
//     });
// }