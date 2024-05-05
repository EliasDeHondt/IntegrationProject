import {Flow} from "./FlowObjects";
import {GetFlows} from "../Kiosk/FlowAPI";

const divFlows = document.getElementById("flowContainer") as HTMLDivElement;
const flowType = document.getElementById("flowType") as HTMLParagraphElement;
const btnSubmitFlows = document.getElementById("btnSubmitFlows") as HTMLButtonElement;
let totalFlows: number;

function GenerateOption(flow: Flow): HTMLDivElement {
    const optionsContainer = document.createElement("div");
    optionsContainer.classList.add("form-check");

    const input = document.createElement("input");
    input.classList.add("form-check-input")
    input.type = "checkbox";
    input.id = `flow${flow.id}`
    input.value = `${flow.id}`

    if (flowType && flowType.innerText == "circular") {
        input.type = "radio"
        input.name = `flowRadios`
    }

    const label = document.createElement("label");
    label.classList.add("form-check-label")
    label.htmlFor = `flow${flow.id}`
    label.innerText = `Flow ${flow.id}`

    optionsContainer.appendChild(input);
    optionsContainer.appendChild(label);
    return optionsContainer;
}

export function GenerateOptions(flows: Flow[], flowContainer: HTMLDivElement) {
    totalFlows = 0;
    const options = flows.map(GenerateOption);

    options.forEach(option => {
        if (flowContainer)
            flowContainer.appendChild(option);
        totalFlows++;
    })

    const btnSubmitFlows = document.createElement("input");
    btnSubmitFlows.type = "submit";
    flowContainer.appendChild(btnSubmitFlows);
    
    btnSubmitFlows.onclick = async (event) => {
        await SubmitFlows(event);
    }
}

GetFlows().then((flows) => {GenerateOptions(flows, divFlows)});

async function SubmitFlows(event: MouseEvent) {
    event.preventDefault();
    const selectedFlows: number[] = [];
    if (flowType.innerText == "linear") {
        const options = document.querySelectorAll<HTMLInputElement>('.form-check-input');
        options.forEach((option: HTMLInputElement) => {
            if (option.checked)
                selectedFlows.push(parseInt(option.value));
        })
    } else {
        const options = document.querySelectorAll<HTMLInputElement>('.form-check-input[type="radio"]:checked');
        if (options.length == 1)
            selectedFlows.push(parseInt((options[0] as HTMLInputElement).value))
    }
    console.log(selectedFlows)
}


