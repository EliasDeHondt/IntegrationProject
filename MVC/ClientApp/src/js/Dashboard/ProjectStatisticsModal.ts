import {Modal} from "bootstrap";

let statisticsButtons: NodeListOf<HTMLButtonElement>;

const statisticsModal = new Modal(document.getElementById('projectStatisticsModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
})

const btnCloseStatistics = document.getElementById("btnCloseStatistics") as HTMLButtonElement;

export function initializeStatisticsButtons() {
    statisticsButtons = document.querySelectorAll("#btnGraphProject");
    statisticsButtons.forEach((button) => {
        button.onclick = () => {
            statisticsModal.show();
        }
    })
}

btnCloseStatistics.onclick = () => {
    statisticsModal.hide();
}

