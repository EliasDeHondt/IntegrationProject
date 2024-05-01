import {MainTheme} from "./Types/ThemeObjects";

const mainThemeContainer = document.getElementById("mainThemeContainer") as HTMLTableSectionElement;

function loadMainThemes() {
    fetch("/api/MainThemes", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => showMainThemes(data))
        .catch(error => console.error("Error:", error))
}


function showMainThemes(mainThemes: MainTheme[]) {
    mainThemeContainer.innerHTML = "";
    mainThemes.forEach(theme => addMainTheme(theme));
}

function addMainTheme(theme: MainTheme) {
    mainThemeContainer.innerHTML += `<tr>
                                        <td>${theme.id}</td>
                                        <td>${theme.subject}</td>
                                        <td><a href="/MainTheme/MainTheme/${theme.id}"">Details</a></td>
                                     </tr>`
}

window.onload = () => loadMainThemes()