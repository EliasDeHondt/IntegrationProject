import {Modal} from "bootstrap";
import {deleteFlow} from "./API/DeleteFlowAPI";
import {getFlows} from "./FlowCreator";

let deleteButtons: NodeListOf<HTMLButtonElement>;
let id: number;

const deleteFlowModal = new Modal(document.getElementById("deleteFlowModal")!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
})
const butConfirmDeleteFlow = document.getElementById("btnConfirmDeleteFlow") as HTMLButtonElement
const butCancelDeleteFlowModal = document.getElementById("btnCancelDeleteFlowModal") as HTMLButtonElement

export function initializeDeleteButtons() {
    deleteButtons = document.querySelectorAll(".flow-card-delete-btn");
    deleteButtons.forEach((button) => {
        button.onclick = () => {
            let card = button.parentNode as HTMLDivElement;
            id = parseInt(card.dataset.flowId as string);
            deleteFlowModal.show();
        }
    })
}

butConfirmDeleteFlow.onclick = () => {
    deleteFlow(id)
        .then(() => {
            getFlows(0);
            deleteFlowModal.hide();
        });
}

butCancelDeleteFlowModal.onclick = () => {
    deleteFlowModal.hide();
}
