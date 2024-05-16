import {Modal} from "bootstrap";
import Chart from 'chart.js/auto';
import {initializeDeleteButtons} from "../CreateFlow/DeleteFlowModal";
import {Project} from "../Types/ProjectObjects";
import * as API from "../Dashboard/API/CreateUserModalAPI";
import {Flow} from "../Flow/FlowObjects";

const barCtx = document.getElementById('barChart') as HTMLCanvasElement;
const lineCtx = document.getElementById('lineChart') as HTMLCanvasElement;
const doughnutCtx = document.getElementById('doughnutChart') as HTMLCanvasElement;
const radarCtx = document.getElementById('radarChart') as HTMLCanvasElement;

const selectFlow = document.getElementById("selectFlow") as HTMLSelectElement;
var flows: Flow[] 
function chartData(label: string, labels: string[], data: string[]) {
    //data for the chart
    return {
        labels: labels,
        datasets: [{
            label: label,
            data: data,
            backgroundColor: 'rgba(54, 162, 235, 0.2)',
            borderColor: 'rgba(54, 162, 235, 1)',
            borderWidth: 1
        }]
    };
}

//draw
function drawBarChart(titel:string,chartData: { labels: string[]; datasets: { label: string; data: string[]; backgroundColor: string; borderColor: string; borderWidth: number; }[]; }){
    const barChart = new Chart(barCtx, {
        type: 'bar',
        data: chartData,
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        font: {
                            size: 22
                        }
                    }
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: titel,
                    font: {
                        size: 24
                    }
                }
                
            }
        }
    });
}
function drawLineChart(titel:string,chartData: { labels: string[]; datasets: { label: string; data: string[]; backgroundColor: string; borderColor: string; borderWidth: number; }[]; }){
    var lineChart = new Chart(lineCtx, {
        type: 'line',
        data: chartData,
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        font: {
                            size: 22
                        }
                    }
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: titel,
                    font: {
                        size: 24
                    }
                }

            }
        }
    });
}
function drawDoughnutChart(titel:string,chartData: { labels: string[]; datasets: { label: string; data: string[]; backgroundColor: string; borderColor: string; borderWidth: number; }[]; }){
    var doughnutChart = new Chart(doughnutCtx, {
        type: 'doughnut',
        data: chartData,
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        font: {
                            size: 22
                        }
                    }
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: titel,
                    font: {
                        size: 24
                    }
                }

            }
        }
    });
}
function drawRadarChart(titel:string,chartData: { labels: string[]; datasets: { label: string; data: string[]; backgroundColor: string; borderColor: string; borderWidth: number; }[]; }){
    var radarChart = new Chart(radarCtx, {
        type: 'radar',
        data: chartData,
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        font: {
                            size: 22
                        }
                    }
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: titel,
                    font: {
                        size: 24
                    }
                }

            }
        }
    });
}

//get data
export async function GetCountStepsPerFlow(labels: string[]){
    console.log("Fetching count steps...")
    await fetch("/api/Statistics/GetCountStepsPerFlow", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => drawBarChart("Aantal Steps per Flow",chartData('Aantal Steps', labels, data)))
        .catch(error => console.error("Error:", error))
}

export async function GetCountParticipationsPerFlow(labels: string[]){
    console.log("Fetching count participations...")
    await fetch("/api/Statistics/GetCountParticipationsPerFlow", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => drawLineChart("Aantal Participations per Flow",chartData('Aantal Participations', labels, data)))
        .catch(error => console.error("Error:", error))
}

export async function GetNamesPerFlow(){
    console.log("Fetching flownames...")
    await fetch("/api/Statistics/GetNamesPerFlow", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(labels => {
            GetCountStepsPerFlow(labels)
            GetCountParticipationsPerFlow(labels)
            flows = labels
        } )
        .catch(error => console.error("Error:", error))
}
// function drawCharts(labels: string[],data: string[]){
//     drawBarChart("Aantal Steps per Flow",chartData('Aantal Steps', labels, data))
//     drawLineChart("Aantal Steps",chartData('Step types', labels, data))
//     drawDoughnutChart("Aantal Steps",chartData('Step types', labels, data))
//     drawRadarChart("Aantal Steps",chartData('Step types', labels, data))
// }

function getSelectedFlows(): number[] {
    let flowIds: number[] = [];
    for (let i = 0; i < selectFlow.options.length; i++) {
        if (selectFlow.options[i].selected) {
            flowIds.push(Number(selectFlow.options[i].value));
        }
    }
    return flowIds
}
function fillDropdownFlows(data: Flow[]) {
    for (let i = 0; i < data.length; i++) {
        let option = document.createElement("option");
        option.value = data[i].id.toString();
        option.text = flows.toString();
        selectFlow.appendChild(option);
    }
}

getSelectedFlows()
// GetNamesPerFlow().then(
//     fillDropdownFlows(flows)
// );
// GetNamesPerFlow()
