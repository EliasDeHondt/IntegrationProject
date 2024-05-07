import {SubTheme} from "../../Types/ProjectObjects";

function generateCard(subTheme: SubTheme): HTMLDivElement{
    const cardContainer = document.createElement("div");
    cardContainer.classList.add("col", "mt-3", "mb-3", "embla__slide");
    
    
    const card = document.createElement("div");
    card.classList.add("card", "border-black", "border-2", "bgAccent", "h-100");
    card.style.height = "150px";

    const deleteButton = document.createElement("button");
    deleteButton.classList.add("border-0", "p-0", "position-absolute", "top-0", "end-0", "me-2");
    deleteButton.style.background = "none";
    deleteButton.innerHTML = `<i class="bi bi-folder-minus" style="color: white;"></i>`;
    //deleteButton.addEventListener("click", onDelete);
    
    const cardBody = document.createElement("div");
    cardBody.classList.add("card-body", "align-items-center", "d-flex", "justify-content-center");
    
    const detailDiv = document.createElement("div");
    const editLink = document.createElement("a");
    editLink.href = "/SubTheme/Subtheme/" + subTheme.id;
    editLink.textContent = subTheme.subject;
    editLink.style.color = "white";
    

    const enterButton = document.createElement("button");
    enterButton.classList.add("border-0", "p-0");
    enterButton.style.background = "none";
    enterButton.innerHTML = `<i class="bi bi-folder" style="color: white; font-size: 10vh;"></i>`;
    enterButton.addEventListener("click", () => {
        window.location.href = "/SubTheme/Subtheme/" + subTheme.id;
    });
    
    const title = document.createElement("p")
    title.className = "text-center text-white";
    title.style.marginTop = "-4vh"
    title.style.marginBottom = "0"
    title.textContent = subTheme.subject;
    
    enterButton.appendChild(title)
    detailDiv.appendChild(enterButton);
    
    card.appendChild(deleteButton);
    cardBody.appendChild(detailDiv);
    card.appendChild(cardBody);
    cardContainer.appendChild(card)
    return cardContainer;
}

export function generateCards(subThemes: SubTheme[], subThemeRoulette: HTMLDivElement) {
    const cards = subThemes.map(generateCard);
    
    cards.forEach(card => {
        subThemeRoulette.appendChild(card);
    })
}

export function resetCards(subThemes: SubTheme[], subThemeRoulette: HTMLDivElement){
    let length = subThemeRoulette.children.length
    for (let i = length - 1; i > 0; i--){
        subThemeRoulette.children[i].remove();
    }
    generateCards(subThemes, subThemeRoulette);
}

export async function getSubThemesForProject(id: number): Promise<SubTheme[]>{
    return await fetch("/api/SubThemes/GetSubThemesForProject/" + id, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    }).then(response => response.json())
        .then(data => {
            return data;
        })
}