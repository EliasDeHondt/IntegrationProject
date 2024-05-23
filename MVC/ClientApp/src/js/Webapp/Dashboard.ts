import {GetFeed, GetFeedIds, GetLoggedInUser, GetRandomFeed} from "./WebAppAPI";
import {addReactionFunctionality, addLikeFunctionality, generateIdeaCard, generateNavButton} from "./Util";
import {Feed} from "../Types/WebApp/Types";
import {PostIdea} from "./Ideas";

const ideaContainer = document.getElementById("ideaContainer") as HTMLDivElement;
const navContainer = document.getElementById("navContainer") as HTMLDivElement;
const titleHeader = document.getElementById("headerTitle") as HTMLHeadingElement;
const btnPlaceIdea = document.getElementById("btnPlaceIdea") as HTMLButtonElement;
const textArea = document.getElementById("textIdea") as HTMLTextAreaElement;
let feedId: number;

btnPlaceIdea.onclick = async () => {
    let text = textArea.value
    let user = await GetLoggedInUser();
    if(text.trim() != "") {
        PostIdea(feedId, text).then(idea => {
            ideaContainer.prepend(generateIdeaCard(idea, user.email));
            addReactionFunctionality();
            addLikeFunctionality();
        })
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
        addReactionFunctionality();
        addLikeFunctionality();
    })
}