import {Flow} from "../Flow/FlowObjects";

const flowContainer = document.getElementById("flowContainer") as HTMLTableSectionElement;
let themeId = Number((document.getElementById("subThemeId") as HTMLSpanElement).innerText);

function loadFlows(id: number) {
    fetch(`/api/SubThemes/${id}/Flows`, {
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
                                   <td>${flow.flowType.toString()}</td>
                                   <td><a href="/Flow/Step/${flow.id}">Start Flow</a></td>
                                </tr>`
}

window.onload = () => loadFlows(themeId)