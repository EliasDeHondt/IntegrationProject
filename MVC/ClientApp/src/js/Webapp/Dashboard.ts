import {GetFeed, GetFeedIds, GetLoggedInUser, GetRandomFeed} from "./WebAppAPI";
import {addHoverEffect, addLikeFunctionality, generateIdeaCard, generateImage, generateNavButton} from "./Util";
import {Feed} from "../Types/WebApp/Types";
import {PostIdea} from "./Ideas";
import {readFileAsBase64} from "../Util";

const ideaContainer = document.getElementById("ideaContainer") as HTMLDivElement;
const navContainer = document.getElementById("navContainer") as HTMLDivElement;
const titleHeader = document.getElementById("headerTitle") as HTMLHeadingElement;
const btnPlaceIdea = document.getElementById("btnPlaceIdea") as HTMLButtonElement;
const fileInput = document.getElementById("file-input") as HTMLInputElement;
const imageContainer = document.getElementById("imageContainer") as HTMLDivElement;
const webappImage = document.getElementById("webapp-idea-image") as HTMLDivElement;
const btnRemoveImage = document.getElementById("btnRemoveImage") as HTMLButtonElement;
let feedId: number;

btnPlaceIdea.onclick = async () => {
    let user = await GetLoggedInUser();
    PostIdea(feedId).then(idea => {
        ideaContainer.prepend(generateIdeaCard(idea, user.email));
        addHoverEffect();
        addLikeFunctionality();
        fileInput.value = "";
        imageContainer.innerHTML = "";
        btnRemoveImage.classList.add("visually-hidden");
        webappImage.classList.add("visually-hidden");
    })
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
    if(base64){
        btnRemoveImage.classList.remove("visually-hidden");
        webappImage.classList.remove("visually-hidden");
        imageContainer.appendChild(generateImage(base64));
    } else {
        btnRemoveImage.classList.add("visually-hidden");
        webappImage.classList.add("visually-hidden");
    }
}

GetRandomFeed().then(feed => {
    generateIdeas(feed);
    titleHeader.innerHTML = feed.title
    feedId = feed.id
})

GetFeedIds().then(feeds => {
    feeds.forEach(feed => {
        navContainer.appendChild(generateNavButton(feed));
        addGetFeedButtons();
    })
})

function addGetFeedButtons(){
    let btns = document.getElementsByClassName("webapp-nav-button") as HTMLCollectionOf<HTMLButtonElement>
    for(const element of btns) {
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

async function generateIdeas(feed: Feed){
    let user = await GetLoggedInUser();
    ideaContainer.innerHTML = "";
    feed.ideas.forEach(idea => {
        ideaContainer.appendChild(generateIdeaCard(idea, user.email));
        addHoverEffect();
        addLikeFunctionality();
    })
}