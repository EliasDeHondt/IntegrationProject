/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

let subThemeList = document.getElementById('subthemeContainer') as HTMLTableElement;
let flowList = document.getElementById('flowContainer') as HTMLTableElement;
let themeId = Number((document.getElementById("themeId") as HTMLHeadingElement).innerText);

function loadSubThemes(id: number) {
    fetch(`/api/Themes/${id}/SubThemes`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => {
            if (response.ok) return response.json();
        })
        .then(themes => {
            let subthemes = "";
            for (const theme of themes) {
                subthemes += `
                    <tr>
                        <td>${theme.id}</td>
                        <td>${theme.subject}</td>
                    </tr>
                `
            }
            subThemeList.innerHTML += subthemes;
        })
        .catch(error => console.error("Error:", error))
}

loadSubThemes(themeId);