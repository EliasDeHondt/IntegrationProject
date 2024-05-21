import {Feed, Idea} from "../Types/WebApp/Types";

export function generateIdeaCard(idea: Idea): HTMLDivElement {
    let div = document.createElement("div");
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
                    <span>${idea.text}</span>
                </div>
                <div class="row mt-2">
                    <div class="col-2">
                        <div class="comment-react ms-2">
                            <button>
                                <svg
                                    width="5vh" height="5vh" viewBox="0 0 24 24" fill="none">
                                    <path
                                        d="M19.4626 3.99415C16.7809 2.34923 14.4404 3.01211 13.0344 4.06801C12.4578 4.50096 12.1696 4.71743 12 4.71743C11.8304 4.71743 11.5422 4.50096 10.9656 4.06801C9.55962 3.01211 7.21909 2.34923 4.53744 3.99415C1.01807 6.15294 0.221721 13.2749 8.33953 19.2834C9.88572 20.4278 10.6588 21 12 21C13.3412 21 14.1143 20.4278 15.6605 19.2834C23.7783 13.2749 22.9819 6.15294 19.4626 3.99415Z"
                                        stroke="white"
                                        fill="white">
                                    </path>
                                </svg>
                            </button>
                            <span id="amountLikes">${idea.likes.length}</span>
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

export function generateNavButton(feed: Feed): HTMLButtonElement{
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

export function addHoverEffect() {
    let btn = document.getElementsByClassName("btnReaction") as HTMLCollectionOf<HTMLButtonElement>;
    let icons = document.getElementsByClassName("iconReaction") as HTMLCollectionOf<SVGSVGElement>;
    for (let i = 0; i < btn.length; i++) {
        btn[i].onmouseover = () => {
            icons[i].classList.replace("bi-chat-dots", "bi-chat-dots-fill");
        }
        btn[i].onmouseout = () => {
            icons[i].classList.replace("bi-chat-dots-fill", "bi-chat-dots")
        }

    }
}