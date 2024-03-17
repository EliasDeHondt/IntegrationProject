import {Flow} from "./ThemeObjects";

const flowContainer = document.getElementById("subThemeContainer") as HTMLTableSectionElement;
let subThemeId = Number((document.getElementById("subThemeId") as HTMLSpanElement).innerText);

function loadFlows(subId: number) {
    fetch(`/api/SubThemes/${subId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => showFlows(data))
        .catch(error => console.error("Error:", error))
}


function showFlows(flows: Flow[]) {
    flowContainer.innerHTML = "";
    flows.forEach(flow => addFlow(flow));
}

function addFlow(flow: Flow) {
    flowContainer.innerHTML += `<tr>
                                   <td>${flow.id}</td>
                                   <td>${flow.flowType}</td>
                                   <td><a href="">Start Flow</a></td>
                                </tr>`
}

window.onload = () => loadFlows(subThemeId)