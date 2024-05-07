import {Modal} from "bootstrap";
import {createPlatform} from "./API/AddPlatformAPI";
import {generatePlatformCards} from "./API/CardAPI";
import {GetSharedPlatforms} from "./API/SharedPlatformAPI";

const modal = new Modal(document.getElementById("ModalCreatePlatform")!, {
    keyboard: false,
    backdrop: "static",
    focus: true
});

const inputName = document.getElementById("inputName") as HTMLInputElement;
const inputLogo = document.getElementById("inputLogo") as HTMLInputElement;
const btnCloseModal = document.getElementById("butCloseCreatePlatformModal") as HTMLButtonElement;
const btnCancelModal = document.getElementById("butCancelCreatePlatformModal") as HTMLButtonElement;
const btnConfirmModal = document.getElementById("butConfirmCreatePlatform") as HTMLButtonElement;
const cardContainer = document.getElementById("containerSharedPlatform") as HTMLDivElement;

export function openModalEventListener(){
    const btnAddModal = document.getElementById("btnAddPlatform") as HTMLButtonElement;
    btnAddModal.onclick = () => {
        modal.show();
    }
}

btnCloseModal.onclick = () => {
    modal.hide();
}

btnCancelModal.onclick = () => {
    clearModal();
    modal.hide();
}

btnConfirmModal.onclick = () => {
    if(validateModal()){
        createPlatform(inputName.value, inputLogo.files ? inputLogo.files[0] : undefined)
            .then(() => {
                GetSharedPlatforms().then(platforms => {
                    cardContainer.innerHTML = generatePlatformCards(platforms);
                    openModalEventListener()
                })
            })
    }
}

function validateModal(): boolean {
    let result = true;
    if (inputName.value == "") {
        document.getElementById("nameWarning")!.textContent = "Please enter a name";
        result =  false;
    }
    
    if(inputLogo.value == ""){
        document.getElementById("logoWarning")!.textContent = "Please upload a logo";
        result = false;
    }
    return result;
}

function clearModal(){
    inputName.value = "";
    inputLogo.value = "";
    document.getElementById("nameWarning")!.textContent = "";
    document.getElementById("logoWarning")!.textContent = "";
}
