const btnReaction = document.getElementById("btnReaction") as HTMLButtonElement;
const iconReaction = document.getElementById("iconReaction") as HTMLElement;

btnReaction.onmouseover = () => {
    iconReaction.classList.replace("bi-chat-dots", "bi-chat-dots-fill");
}

btnReaction.onmouseout = () => {
    iconReaction.classList.replace("bi-chat-dots-fill", "bi-chat-dots");
}