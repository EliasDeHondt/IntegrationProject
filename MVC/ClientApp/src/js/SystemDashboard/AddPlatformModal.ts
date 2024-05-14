import {Modal} from "bootstrap";
import {createPlatform} from "./API/AddPlatformAPI";
import {generatePlatformCards} from "./API/CardAPI";
import {GetSharedPlatforms} from "./API/SharedPlatformAPI";
import {isEmailInUse} from "../API/UserAPI";

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

const inputUserName = document.getElementById("inputUserName") as HTMLInputElement;
const inputEmail = document.getElementById("inputEmail") as HTMLInputElement;
const inputPassword = document.getElementById("inputPassword") as HTMLInputElement;

const nameWarning = document.getElementById('usernameWarning') as HTMLElement;
const emailWarning = document.getElementById('emailWarning') as HTMLElement;
const passwordWarning = document.getElementById('passwordWarning') as HTMLElement;

export function openModalEventListener() {
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
    validateModal().then(result => {
        if (result) {
            createPlatform(inputName.value, inputUserName.value, inputPassword.value, inputEmail.value, inputLogo.files ? inputLogo.files[0] : undefined)
                .then(() => {
                    GetSharedPlatforms().then(platforms => {
                        cardContainer.innerHTML = generatePlatformCards(platforms);
                        modal.hide();
                        openModalEventListener()
                    })
                })
        }
    })
}

async function validateModal(): Promise<boolean> {
    let result = true;
    if (inputName.value == "") {
        document.getElementById("nameWarning")!.textContent = "Please enter a name";
        result = false;
    }

    if (inputLogo.value == "") {
        document.getElementById("logoWarning")!.textContent = "Please upload a logo";
        result = false;
    }

    if (inputUserName.value.trim() === '') {
        nameWarning.textContent = 'Please enter a name';
        result = false;
    } else {
        nameWarning.textContent = '';
    }

    let emailRegex: RegExp = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (inputEmail.value.trim() === '') {
        emailWarning.textContent = 'Please enter an email';
        result = false;
    } else if (!emailRegex.test(inputEmail.value)) {
        emailWarning.textContent = 'Please enter a valid email';
        result = false;
    } else if (await isEmailInUse(inputEmail.value)) {
        emailWarning.textContent = 'This email is already in use';
        result = false;
    } else {
        emailWarning.textContent = '';
    }

    let passwordRegex: RegExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{10,}$/

    if (inputPassword.value.trim() === '') {
        passwordWarning.textContent = 'Please enter a password';
        result = false;
    } else if (!passwordRegex.test(inputPassword.value)) {
        passwordWarning.textContent = 'The password needs to be 10 characters long and contain at least one uppercase letter, one lowercase letter, one number and one special character';
        result = false;
    } else {
        passwordWarning.textContent = '';
    }

    return result;
}

function clearModal() {
    inputName.value = "";
    inputLogo.value = "";
    document.getElementById("nameWarning")!.textContent = "";
    document.getElementById("logoWarning")!.textContent = "";
}
