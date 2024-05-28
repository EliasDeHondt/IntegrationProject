import {Step} from "../../Flow/Step/StepObjects";

export async function getStepsFromFlow(flowId: number): Promise<Step[]> {
    return await fetch(`/api/Steps/GetStepsFromFlow/${flowId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
}

export async function getStepByNumber(flowId: number, stepNr: number): Promise<Step> {
    return await fetch(`/api/Steps/GetNextStep/${flowId}/${stepNr}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
}

export async function getStepId(flowId: number, stepNr: number): Promise<number> {
    return await fetch(`/EditFlows/GetStepId/${flowId}/${stepNr}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data
        })
}

export async function addChoice(flowId: number, stepNr: number) {
    await fetch(`/EditFlows/CreateChoice/${flowId}/${stepNr}`, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .catch(error => console.error("Error:", error))
}

export async function addInformation(flowId: number, stepNr: number, type: string) {
    await fetch(`/EditFlows/CreateInformation/${flowId}/${stepNr}/${type}`, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .catch(error => console.error("Error:", error))
}

export async function updateInformationStep(step: Step) {
    await fetch(`/api/Steps/UpdateInformationStep`, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(step)
    })
}

export async function updateQuestionStep(step: Step) {
    await fetch(`/api/Steps/UpdateQuestionStep`, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(step)
    })
}
export async function updateQuestionStepByNumber(stepId: number, stepNumber: number){//flowId: number, stepNr: number) {
    await fetch(`/api/Steps/UpdateQuestionStepByNumber/${stepId}/${stepNumber}`, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
}
export async function updateInfoStepByNumber(stepId: number, stepNumber: number){//flowId: number, stepNr: number) {
    await fetch(`/api/Steps/UpdateInfoStepByNumber/${stepId}/${stepNumber}`, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
}