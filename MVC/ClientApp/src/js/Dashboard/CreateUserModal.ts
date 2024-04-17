import {Dropdown, Modal} from "bootstrap";

const CreateUserModal = new Modal(document.getElementById('CreateUserModal')!, {
    keyboard: false,
    focus: true,
    backdrop: "static"
});

const butCreateUser = document.getElementById("butCreateUser") as HTMLButtonElement;
const butCloseCreateUserModal = document.getElementById("butCloseCreateUserModal") as HTMLButtonElement;
const butCancelCreateUserModal = document.getElementById("butCancelCreateUserModal") as HTMLButtonElement;
const butConfirmCreateUser = document.getElementById("butConfirmCreateUser") as HTMLButtonElement;

const radioAdmin = document.getElementById("radioAdmin") as HTMLInputElement;
const radioFacilitator = document.getElementById("radioFacilitator") as HTMLInputElement;
const facilitatorContainer = document.getElementById("facilitatorContainer") as HTMLDivElement;

butCreateUser.onclick = () => {
    CreateUserModal.show();
}

butCloseCreateUserModal.onclick = () => {
    CreateUserModal.hide();
}

butCancelCreateUserModal.onclick = () => {
    // Empty fields
    
    CreateUserModal.hide();
}

butConfirmCreateUser.onclick = () => {
    // API call to create user
    
    CreateUserModal.hide();
}

radioAdmin.onclick = () => {
    facilitatorContainer.innerHTML = "";
}

radioFacilitator.onclick = () => {
    
    let butDdl = document.createElement("button");
    butDdl.className = "btn btn-success dropdown-toggle";
    butDdl.type = "button";
    butDdl.setAttribute("data-bs-toggle", "dropdown");
    
    let ulProjects = document.createElement("ul");
    ulProjects.classList.add("dropdown-menu");
    
    // Generate HTML For each project.
    
    // Demo html:
    let project = document.createElement("li");
    let label = document.createElement("label");
    let input = document.createElement("input");
    input.type = "checkbox";
    label.appendChild(input);
    label.append("test");
    project.appendChild(label);
    ulProjects.appendChild(project);
    
    facilitatorContainer.appendChild(butDdl);
    facilitatorContainer.appendChild(ulProjects);
    let dropdownEl= new Dropdown(butDdl);
    dropdownEl.show()
}

function handleAllowFocusClick(event: MouseEvent) {
    event.stopPropagation();
}

let t = document.getElementsByClassName("allow-focus") as HTMLCollectionOf<HTMLElement>
Array.from(t).forEach(e => e.addEventListener('click', handleAllowFocusClick));