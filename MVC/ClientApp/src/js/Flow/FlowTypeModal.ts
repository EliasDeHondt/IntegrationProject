import {Modal} from "bootstrap";
import {GenerateOptions, SubmitFlows} from "./ChooseFlow";
import {code} from "./Facilitator";
import {Flow} from "./FlowObjects";
import {GetFlowsForProject} from "../Kiosk/FlowAPI";

export const flowTypeModal = new Modal(document.getElementById('flowTypeModal')!, {
    keyboard: false,
    backdrop: "static"
});

const btnSubmitFlows = document.getElementById("btnSubmitFlows") as HTMLButtonElement;
const radioLinear = document.getElementById("radioLinear") as HTMLInputElement;
const radioCircular = document.getElementById("radioCircular") as HTMLInputElement;
const radioPhysical = document.getElementById("radioPhysical") as HTMLInputElement; 
const divFlows = document.getElementById("flowOptionsContainer") as HTMLDivElement;
let projectId: number = 0;
let flows: Flow[];

if (btnSubmitFlows)
    btnSubmitFlows.onclick = () => {
        SubmitFlows(code)
        flowTypeModal.hide();
    }


function FillOptions(type: string) {
    GenerateOptions(flows, divFlows, type)
}

radioLinear.onchange = () => {
    FillOptions("linear");
}

radioCircular.onchange = () => {
    FillOptions("circular");
}

radioPhysical.onchange = () => {
    FillOptions("physical");
}

export async function setProjectId(id: number){
    projectId = id;
    flows = await GetFlowsForProject(projectId);
}