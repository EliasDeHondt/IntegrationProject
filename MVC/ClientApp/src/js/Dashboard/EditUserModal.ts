let editButtons;


export function initializeEditButtons() {
    editButtons = document.getElementsByClassName("editUser") as HTMLCollectionOf<HTMLButtonElement>;
    for (let i = 0; i < editButtons.length; i++) {
        editButtons[i].onclick = () => {
            console.log("edit clicked")
        }
    }
}


