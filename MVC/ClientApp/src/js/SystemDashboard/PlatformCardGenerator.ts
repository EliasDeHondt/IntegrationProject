import {GetSharedPlatforms} from "./API/SharedPlatformAPI";
import {generatePlatformCards} from "./API/CardAPI";

const cardContainer = document.getElementById("containerSharedPlatform") as HTMLDivElement;

GetSharedPlatforms().then(platforms => {
    cardContainer.innerHTML = generatePlatformCards(platforms);
})