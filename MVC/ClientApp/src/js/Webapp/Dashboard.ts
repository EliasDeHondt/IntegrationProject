import {GetRandomFeed} from "./WebAppAPI";
import {generateIdeaCard} from "./IdeaUtil";

const btnReaction = document.getElementById("btnReaction") as HTMLButtonElement;
const iconReaction = document.getElementById("iconReaction") as HTMLElement;
const ideaContainer = document.getElementById("ideaContainer") as HTMLDivElement;

btnReaction.onmouseover = () => {
    iconReaction.classList.replace("bi-chat-dots", "bi-chat-dots-fill");
}

btnReaction.onmouseout = () => {
    iconReaction.classList.replace("bi-chat-dots-fill", "bi-chat-dots");
}

GetRandomFeed().then(feed => {
    feed.ideas.forEach(idea => {
        ideaContainer.appendChild(generateIdeaCard(idea));
    })
})