import {GetSharedPlatforms} from "./API/SharedPlatformAPI";
import {generatePlatformCards} from "./API/CardAPI";
import {openModalEventListener} from "./AddPlatformModal";

const cardContainer = document.getElementById("containerSharedPlatform") as HTMLDivElement;

GetSharedPlatforms().then(platforms => {
    cardContainer.innerHTML += generatePlatformCards(platforms);
    openModalEventListener()
})