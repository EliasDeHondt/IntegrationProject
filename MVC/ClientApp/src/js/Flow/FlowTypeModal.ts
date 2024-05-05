import {Modal} from "bootstrap";

export const flowTypeModal = new Modal(document.getElementById('flowTypeModal')!, {
    keyboard: false,
    backdrop: "static"
});

const btnLinearFlow = document.getElementById("btnLinearFlow") as HTMLButtonElement;
const btnCircularFlow = document.getElementById("btnCircularFlow") as HTMLButtonElement;

btnLinearFlow.onclick = () => {
    flowTypeModal.hide();
    window.location.href = `/Flow/FlowPicker?type=linear`
}

btnCircularFlow.onclick = () => {
    flowTypeModal.hide();
    window.location.href = `/Flow/FlowPicker?type=circular`
}