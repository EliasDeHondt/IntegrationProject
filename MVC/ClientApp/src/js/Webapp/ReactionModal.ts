import {Modal} from "bootstrap";
import {GetReactions, PostReaction} from "./WebAppAPI";
import {generateReaction} from "./Util";

const modalDiv = document.getElementById("modalReplies") as HTMLDivElement;
const modal = new Modal(modalDiv, {
    keyboard: false,
    backdrop: true
});
const btnCloseReplyModal = document.getElementById("btnCloseReplyModal") as HTMLButtonElement;

const textReply = document.getElementById("textReply") as HTMLInputElement;
const btnPostReply = document.getElementById("btnPostReply") as HTMLButtonElement;

const container = document.getElementById("replyContainer") as HTMLDivElement;

export function OpenModal(ideaId: number) {
    modal.show()
    GetReactions(ideaId).then(reactions => {
        container.innerHTML = "";
        reactions.forEach(reaction => {
            container.appendChild(generateReaction(reaction));
        })
    })
    
    btnPostReply.onclick = () => {
        let text = textReply.value;
        if(text.trim() != "") {
            PostReaction(ideaId, text).then(reaction => {
                container.prepend(generateReaction(reaction))
                textReply.value = "";
            }); 
        }
    }
    
}

export function CloseModal() {
    modal.hide();
}

modalDiv.addEventListener('hidden.bs.modal', event => {
    let container = document.getElementById("replyContainer") as HTMLDivElement;
    container.innerHTML = "";
})

btnCloseReplyModal.onclick = () => {
    CloseModal()
}