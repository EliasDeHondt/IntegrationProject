import {GetFeed, GetFeedIds, GetRandomFeed} from "./WebAppAPI";
import {addHoverEffect, generateIdeaCard, generateNavButton} from "./Util";
import {Feed} from "../Types/WebApp/Types";

const ideaContainer = document.getElementById("ideaContainer") as HTMLDivElement;
const navContainer = document.getElementById("navContainer") as HTMLDivElement;
const titleHeader = document.getElementById("headerTitle") as HTMLHeadingElement;

GetRandomFeed().then(feed => {
    generateIdeas(feed);
    titleHeader.innerHTML = feed.title
})

GetFeedIds().then(feeds => {
    feeds.forEach(feed => {
        navContainer.appendChild(generateNavButton(feed));
        addGetFeedButtons();
    })
})

function addGetFeedButtons(){
    let btns = document.getElementsByClassName("webapp-nav-button") as HTMLCollectionOf<HTMLButtonElement>
    for(let i = 0; i < btns.length; i++){
        btns[i].onclick = () => {
            let id = parseInt(btns[i].getAttribute("data-id")!);
            GetFeed(id).then(feed => {
                generateIdeas(feed);
                titleHeader.innerHTML = feed.title
            })
        }
    }
}

function generateIdeas(feed: Feed){
    ideaContainer.innerHTML = "";
    feed.ideas.forEach(idea => {
        ideaContainer.appendChild(generateIdeaCard(idea));
        addHoverEffect();
    })
}