import {Feed, Idea, Reaction} from "../Types/WebApp/Types";
import {CreateLike, DeleteLike} from "./WebAppAPI";
import {OpenModal} from "./ReactionModal";

export function generateIdeaCard(idea: Idea, email: string): HTMLDivElement {
    let image: HTMLImageElement | null;
    if(idea.image){
        image = generateImage(idea.image);
    } else {
        image = null
    }
    
    let imagediv = document.createElement("div");
    if(image) imagediv.appendChild(image);
    
    let div = document.createElement("div");
    let liked: boolean = idea.likes.some(like => like.liker.email === email);
    div.classList.add("webapp-card")
    div.innerHTML = `<div class="card-body p-2 ms-2 me-2">
                <div class="row webapp-card-header">
                    <div class="col-1 m-auto">
                        <i class="bi bi-person-circle webapp-util-icon"></i>
                    </div>
                    <div class="col-11 m-auto">
                        <span>${idea.author.name}</span>
                    </div>
                </div>
                <div class="row webapp-card-content">
                    <span class="webapp-card-text">${idea.text}</span>
                    ${image ? imagediv.innerHTML : ""}
                </div>
                <div class="row mt-2">
                    <div class="col-2">
                        <div class="comment-react ms-2">
                            <button class="btnLikes" data-id="${idea.id}" data-liked="${liked}">
                                <svg
                                    width="5vh" height="5vh" viewBox="0 0 24 24" fill="none">
                                    <path
                                        d="M19.4626 3.99415C16.7809 2.34923 14.4404 3.01211 13.0344 4.06801C12.4578 4.50096 12.1696 4.71743 12 4.71743C11.8304 4.71743 11.5422 4.50096 10.9656 4.06801C9.55962 3.01211 7.21909 2.34923 4.53744 3.99415C1.01807 6.15294 0.221721 13.2749 8.33953 19.2834C9.88572 20.4278 10.6588 21 12 21C13.3412 21 14.1143 20.4278 15.6605 19.2834C23.7783 13.2749 22.9819 6.15294 19.4626 3.99415Z"
                                        stroke="white"
                                        fill="white">
                                    </path>
                                </svg>
                            </button>
                            <span class="spanLikes">${idea.likes.length}</span>
                        </div>
                    </div>
                    <div class="col-10">
                        <button class="p-1 btn bg-none shadow-none border-0 btnReaction" data-id="${idea.id}">
                            <i class="bi bi-chat-dots webapp-util-icon chat-dots iconReaction"></i>
                        </button>
                    </div>
                </div>
            </div>`
    return div;
}

export function generateNavButton(feed: Feed): HTMLButtonElement {
    let btn = document.createElement("button");
    btn.classList.add("webapp-nav-button");
    btn.setAttribute("data-id", feed.id.toString());
    btn.innerHTML = `
            ${feed.title} Feed
            <div class="icon">
                <svg
                    height="24"
                    width="24"
                    viewBox="0 0 24 24">
                    <path d="M0 0h24v24H0z" fill="none"></path>
                    <path
                        d="M16.172 11l-5.364-5.364 1.414-1.414L20 12l-7.778 7.778-1.414-1.414L16.172 13H4v-2z"
                        fill="currentColor">
                    </path>
                </svg>
            </div>`
    return btn
}

export function generateImage(image: string): HTMLImageElement {
    let img = document.createElement("img");
    img.classList.add("webapp-image");
    img.src = "data:image/png;base64," + image;
    return img;
}

export function addReactionFunctionality() {
    let btn = document.getElementsByClassName("btnReaction") as HTMLCollectionOf<HTMLButtonElement>;
    let icons = document.getElementsByClassName("iconReaction") as HTMLCollectionOf<SVGSVGElement>;
    for (let i = 0; i < btn.length; i++) {
        btn[i].onmouseover = () => {
            icons[i].classList.replace("bi-chat-dots", "bi-chat-dots-fill");
        }
        btn[i].onmouseout = () => {
            icons[i].classList.replace("bi-chat-dots-fill", "bi-chat-dots")
        }

        btn[i].onclick = () => {
            OpenModal(parseInt(btn[i].getAttribute("data-id")!));
        }
        
    }
}
export function generateReaction(reaction: Reaction): HTMLDivElement {
    let div = document.createElement("div");
    div.classList.add("row");
    div.classList.add("h-25");
    div.classList.add("reply-container");
    div.innerHTML = `<div class="col h-100">
                        <div class="row ps-3 reply-header">
                            ${reaction.author.name}
                        </div>
                        <div class="row ms-1 reply-body">
                            ${reaction.text}
                        </div>
                    </div>`
    return div
}
export function addLikeFunctionality() {
    let btns = document.getElementsByClassName("btnLikes") as HTMLCollectionOf<HTMLButtonElement>;
    let counts = document.getElementsByClassName("spanLikes") as HTMLCollectionOf<HTMLSpanElement>;
    for (let i = 0; i < btns.length; i++) {
        let btn = btns[i];
        let count = counts[i];
        if(btn.getAttribute("data-liked") == "true"){
            let icon = btn.firstElementChild!.firstElementChild! as SVGSVGElement;
            icon.style.fill = "#f5356e";
            btn.onmouseover = () => {
                icon.style.fill = "white";
            }
            btn.onmouseout = () => {
                icon.style.fill = "#f5356e";
            }
        }
        btn.onclick = () => {
            if (btn.getAttribute("data-liked") == "false") {
                addLike(btn, count);
            } else {
                removeLike(btn, count);
            }
        }
    }
}
function addLike(btn: HTMLButtonElement, count: HTMLSpanElement){
    CreateLike(parseInt(btn.getAttribute("data-id")!))
        .then(() => {
            let icon = btn.firstElementChild!.firstElementChild! as SVGSVGElement;
            icon.style.fill = "#f5356e";
            btn.onmouseover = () => {
                icon.style.fill = "white";
            }
            btn.onmouseout = () => {
                icon.style.fill = "#f5356e";
            }
            btn.setAttribute("data-liked", "true");
            count.innerHTML = String(parseInt(count.innerHTML) + 1)
        });
}
function removeLike(btn: HTMLButtonElement, count: HTMLSpanElement){
    DeleteLike(parseInt(btn.getAttribute("data-id")!))
        .then(() => {
            let icon = btn.firstElementChild!.firstElementChild! as SVGSVGElement;
            icon.style.fill = "white";
            btn.onmouseover = () => {
                icon.style.fill = "#f5356e";
            }
            btn.onmouseout = () => {
                icon.style.fill = "white";
            }
            btn.setAttribute("data-liked", "false");
            count.innerHTML = String(parseInt(count.innerHTML) - 1)
        });
}