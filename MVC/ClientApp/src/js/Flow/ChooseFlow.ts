import {Flow} from "./FlowObjects";

let totalFlows: number;
let flowType: string;

function GenerateOption(flow: Flow): HTMLDivElement {
    const optionsContainer = document.createElement("div");
    optionsContainer.classList.add("form-check");

    const input = document.createElement("input");
    input.classList.add("form-check-input")
    input.type = "checkbox";
    input.id = `flow${flow.id}`
    input.value = `${flow.id}`

    if (flowType == "circular" || flowType == "physical") {
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

export function GenerateOptions(flows: Flow[], flowContainer: HTMLDivElement, type: string) {
    flowContainer.innerHTML = "";
    totalFlows = 0;
    flowType = type;
    if(type == "physical") type = "circular";
    let options: HTMLDivElement[] = [];
    flows.forEach(flows => {
        if(flows.flowType.toUpperCase() == type.toUpperCase()){
            options.push(GenerateOption(flows));
        }
    })

    options.forEach(option => {
        if (flowContainer)
            flowContainer.appendChild(option);
        totalFlows++;
    })
}

export function SubmitFlows(connection: signalR.HubConnection, code: string) {
    const selectedFlows: number[] = [];
    if (flowType == "linear") {
        const options = document.querySelectorAll<HTMLInputElement>('.form-check-input[type="checkbox"]:checked');
        options.forEach((option: HTMLInputElement) => {
            selectedFlows.push(parseInt(option.value));
        })
    } else if (flowType == "circular" || flowType == "physical") {
        const options = document.querySelectorAll<HTMLInputElement>('.form-check-input[name="flowRadios"]:checked');
        if (options.length == 1)
            selectedFlows.push(parseInt((options[0] as HTMLInputElement).value))
    }
    connection.invoke("SendSelectedFlowIds", code, selectedFlows, flowType);
}

export function getFlowType(): string {
    return flowType;
}