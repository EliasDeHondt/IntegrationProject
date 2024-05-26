import {GetFeed, GetFeedIds, GetLoggedInUser, GetRandomFeed} from "./WebAppAPI";
import {
    addLikeFunctionality,
    addReactionFunctionality,
    generateIdeaCard,
    generateImage,
    generateNavButton
} from "./Util";
import {Feed} from "../Types/WebApp/Types";
import {PostIdea} from "./Ideas";
import {IsValidIdea} from "./Moderation";
import {delay, readFileAsBase64} from "../Util";
import {Modal} from "bootstrap";

const ideaContainer = document.getElementById("ideaContainer") as HTMLDivElement;
const navContainer = document.getElementById("navContainer") as HTMLDivElement;
const titleHeader = document.getElementById("headerTitle") as HTMLHeadingElement;
const btnPlaceIdea = document.getElementById("btnPlaceIdea") as HTMLButtonElement;
const fileInput = document.getElementById("file-input") as HTMLInputElement;
const imageContainer = document.getElementById("imageContainer") as HTMLDivElement;
const webappImage = document.getElementById("webapp-idea-image") as HTMLDivElement;
const btnRemoveImage = document.getElementById("btnRemoveImage") as HTMLButtonElement;
const textArea = document.getElementById("textIdea") as HTMLTextAreaElement;

const modalInvalidIdea = new Modal(document.getElementById("modalInvalidIdea") as HTMLDivElement, {
    keyboard: true,
    backdrop: false,
});
const btnCloseInvalidIdea = document.getElementById("btnCloseInvalidIdea") as HTMLButtonElement;

btnCloseInvalidIdea.onclick = () => {
    modalInvalidIdea.hide();
}

const feedNumber = document.getElementById("feedNumber") as HTMLDivElement;
let feedId: number;
btnPlaceIdea.onclick = async () => {
    let text = textArea.value.trim()
    let user = await GetLoggedInUser();
    const fileInput = document.getElementById("file-input") as HTMLInputElement;
    if (fileInput.files![0] != null || text != "") {
        IsValidIdea(text).then(res => {
            if (res) {
                PostIdea(feedId, text).then(idea => {
                    ideaContainer.prepend(generateIdeaCard(idea, user.email));
                    addReactionFunctionality();
                    addLikeFunctionality();
                    textArea.value = "";
                    fileInput.value = "";
                    imageContainer.innerHTML = "";
                    btnRemoveImage.classList.add("visually-hidden");
                    webappImage.classList.add("visually-hidden");
                })
            } else {
                modalInvalidIdea.show();
                delay(3000).then(() => modalInvalidIdea.hide());
            }
        })
    }   
}

btnRemoveImage.onclick = () => {
    fileInput.value = "";
    imageContainer.innerHTML = "";
    btnRemoveImage.classList.add("visually-hidden");
    webappImage.classList.add("visually-hidden");
}

fileInput.onchange = async () => {
    let image = fileInput.files![0];
    imageContainer.innerHTML = "";
    let base64 = await readFileAsBase64(image);
    if (base64) {
        btnRemoveImage.classList.remove("visually-hidden");
        webappImage.classList.remove("visually-hidden");
        imageContainer.appendChild(generateImage(base64));
    } else {
        btnRemoveImage.classList.add("visually-hidden");
        webappImage.classList.add("visually-hidden");
    }
}

if(feedNumber.textContent){
    feedId = parseInt(feedNumber.textContent);
    if(feedId > 0){
        GetFeed(feedId).then(feed => {
            generateIdeas(feed);
            titleHeader.innerHTML = feed.title
        })
    } else {
        GetRandomFeed().then(feed => {
            generateIdeas(feed);
            titleHeader.innerHTML = feed.title
            feedId = feed.id
        })
    }
}

GetFeedIds().then(feeds => {
    feeds.forEach(feed => {
        navContainer.appendChild(generateNavButton(feed));
        addGetFeedButtons();
    })
})

function addGetFeedButtons() {
    let btns = document.getElementsByClassName("webapp-nav-button") as HTMLCollectionOf<HTMLButtonElement>
    for (const element of btns) {
        element.onclick = () => {
            let id = parseInt(element.getAttribute("data-id")!);
            GetFeed(id).then(feed => {
                generateIdeas(feed);
                titleHeader.innerHTML = feed.title
                feedId = feed.id
            })
        }
    }
}

async function generateIdeas(feed: Feed) {
    let user = await GetLoggedInUser();
    ideaContainer.innerHTML = "";
    feed.ideas.forEach(idea => {
        ideaContainer.appendChild(generateIdeaCard(idea, user.email));
        addReactionFunctionality();
        addLikeFunctionality();
    })
}