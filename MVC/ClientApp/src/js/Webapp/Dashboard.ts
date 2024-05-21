import {GetFeed, GetFeedIds, GetLoggedInUser, GetRandomFeed} from "./WebAppAPI";
import {addHoverEffect, addLikeFunctionality, generateIdeaCard, generateNavButton} from "./Util";
import {Feed} from "../Types/WebApp/Types";
import {PostIdea} from "./Ideas";

const ideaContainer = document.getElementById("ideaContainer") as HTMLDivElement;
const navContainer = document.getElementById("navContainer") as HTMLDivElement;
const titleHeader = document.getElementById("headerTitle") as HTMLHeadingElement;
const btnPlaceIdea = document.getElementById("btnPlaceIdea") as HTMLButtonElement;
let feedId: number;

btnPlaceIdea.onclick = async () => {
    let user = await GetLoggedInUser();
    PostIdea(feedId).then(idea => {
        ideaContainer.prepend(generateIdeaCard(idea, user.email));
        addHoverEffect();
    })
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