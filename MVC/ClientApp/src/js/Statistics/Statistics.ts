import {Modal} from "bootstrap";
import Chart from 'chart.js/auto';
import {initializeDeleteButtons} from "../CreateFlow/DeleteFlowModal";

const barCtx = document.getElementById('barChart') as HTMLCanvasElement;
const lineCtx = document.getElementById('lineChart') as HTMLCanvasElement;
const doughnutCtx = document.getElementById('doughnutChart') as HTMLCanvasElement;
const radarCtx = document.getElementById('radarChart') as HTMLCanvasElement;
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
                    beginAtZero: true
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: titel
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
            plugins: {
                title: {
                    display: true,
                    text: titel
                }
            }}
    });
}
function drawRadarChart(titel:string,chartData: { labels: string[]; datasets: { label: string; data: string[]; backgroundColor: string; borderColor: string; borderWidth: number; }[]; }){
    var radarChart = new Chart(radarCtx, {
        type: 'radar',
        data: chartData,
        options: {
            plugins: {
                title: {
                    display: true,
                    text: titel
                }
            }}
    });
}

//get data
export async function GetCountStepsPerFlow(){
    console.log("Fetching count...")
    await fetch("/api/Statistics/GetCountStepsPerFlow", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => GetNamesPerFlow(data))
        .catch(error => console.error("Error:", error))
}
export async function GetNamesPerFlow(data: string[]){
    console.log("Fetching count...")
    await fetch("/api/Statistics/GetNamesPerFlow", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(labels => drawBarChart("Aantal Steps per Flow",chartData('Aantal Steps', labels, data)))
        .catch(error => console.error("Error:", error))
}
// function drawCharts(labels: string[],data: string[]){
//     drawBarChart("Aantal Steps per Flow",chartData('Aantal Steps', labels, data))
//     drawLineChart("Aantal Steps",chartData('Step types', labels, data))
//     drawDoughnutChart("Aantal Steps",chartData('Step types', labels, data))
//     drawRadarChart("Aantal Steps",chartData('Step types', labels, data))
// }



GetCountStepsPerFlow()