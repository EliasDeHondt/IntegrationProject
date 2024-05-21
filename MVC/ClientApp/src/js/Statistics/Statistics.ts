import Chart from 'chart.js/auto';
import {
    GetAnswerCountsForQuestions,
    GetChoicesNames,
    GetCountParticipationsPerFlow,
    GetCountStepsPerFlow,
    GetNamesPerFlow, GetParticipatoinNames, GetQuestionNames,
    GetQuestionsFromFlow
} from "./API/StatisticAPI";
import {Question} from "../Types/ProjectObjects";

const barCtx = document.getElementById('barChart') as HTMLCanvasElement;
const lineCtx = document.getElementById('lineChart') as HTMLCanvasElement;
const doughnutCtx = document.getElementById('doughnutChart') as HTMLCanvasElement;
const radarCtx = document.getElementById('radarChart') as HTMLCanvasElement;
const pieCtx = document.getElementById('pieChart') as HTMLCanvasElement;

const selectFlow = document.getElementById("selectFlow") as HTMLSelectElement;
const selectQuestion = document.getElementById("selectQuestion") as HTMLSelectElement;
var flowNames: string[]
var colors = [
    'rgba(255, 99, 132, 0.7)',
    'rgba(54, 162, 235, 0.7)',
    'rgba(255, 206, 86, 0.7)',
    'rgba(75, 192, 192, 0.7)',
    'rgba(153, 102, 255, 0.7)',
    'rgba(255, 159, 64, 0.7)',
    'rgba(255, 200, 200, 0.7)',
    'rgba(255, 123, 100, 0.7)'];

function chartData(label: string, labels: string[], data: string[]) {
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

function chartDataColors(label: string, labels: string[], data: string[]) {
    return {
        labels: labels,
        datasets: [{
            label: label,
            data: data,
            backgroundColor: colors,
            borderColor: 'rgba(54, 162, 235, 1)',
            borderWidth: 1
        }]
    };
}
function chartPlugins(titel: string) {
    return {
            title: {
                display: true,
                text: titel,
                font: {
                    size: 24
                }
            },
            legend: {
                labels: {
                    font: {
                        size: 22
                    }
                }
            },
            tooltip: {
                titleFont: {
                    size: 22
                },
                bodyFont: {
                    size: 22
                }
            }
    };
}
function chartScales(){
    return {
            y: {
            beginAtZero: true,
                ticks: {
                font: {
                    size: 22
                }
            }
        }
    };
}

//draw
export function drawBarChart(titel:string,labels: string[], label: string, data: string[]){
    const barChart = new Chart(barCtx, {
        type: 'bar',
        data: chartData(label,labels,data),
        options: {
            scales: chartScales(),
            plugins: chartPlugins(titel)
        }
    });
}

export function drawLineChart(titel:string,labels: string[], label: string, data: string[]){
    var lineChart = new Chart(lineCtx, {
        type: 'line',
        data: chartData(label,labels,data),
        options: {
            scales: chartScales(),
            plugins: chartPlugins(titel)
        }
    });
}
let doughnutChart: Chart<"doughnut", string[], string> | null = null;
export function drawDoughnutChart(titel:string,labels: string[], label: string, data: string[]){
    // @ts-ignore
    if (doughnutChart) {
        doughnutChart.destroy();
    }
    
    doughnutChart = new Chart(doughnutCtx, {
        type: 'doughnut',
        data: chartDataColors(label,labels,data),
        options: {
            scales: chartScales(),
            plugins: chartPlugins(titel)
        }
    });
}
let radarChart: Chart<"radar", string[], string> | null = null;
export function drawRadarChart(titel:string,labels: string[], label: string, data: string[]){

    if (radarChart) {
        radarChart.destroy();
    }
    
     radarChart = new Chart(radarCtx, {
        type: 'radar',
        data: chartData(label,labels,data),
        options: {
            scales: chartScales(),
            plugins: chartPlugins(titel)
        }
    });
}
let pieChart: Chart<"pie", string[], string> | null = null;
export function drawPieChart(titel:string,labels: string[], label: string, data: string[]){

    if (pieChart) {
        pieChart.destroy();
    }

    pieChart = new Chart(pieCtx, {
        type: 'pie',
        data: chartDataColors(label,labels,data),
        options: {
            scales: chartScales(),
            plugins: chartPlugins(titel)
        }
    });
}

export function getSelectedFlows(): number[] {
    let flowIds: number[] = [];
    for (let i = 0; i < selectFlow.options.length; i++) {
        if (selectFlow.options[i].selected) {
            flowIds.push(Number(selectFlow.options[i].value));
        }
    }
    console.log("flowIds: ", flowIds)
    return flowIds
}
export function fillDropdownFlows(data: string[], select: HTMLSelectElement) {
    select.innerHTML = "";

    for (let i = 0; i < data.length; i++) {
        let option = document.createElement("option");
        option.value = i.toString(); 
        option.text = data[i]; 
        select.appendChild(option);
    }
    selectFlow.addEventListener('change', showSelectedFlow);
    showSelectedFlow();
}
export function fillDropdownQuestions(data: Question[], select: HTMLSelectElement) {
    select.innerHTML = "";

    for (let i = 0; i < data.length; i++) {
        let option = document.createElement("option");
        console.log("data[i].text",data[i].id.toString())
        console.log("data[i].text",data[i].question)
        option.value = data[i].id.toString();
        option.text = data[i].question;
        select.appendChild(option);
    }
    selectFlow.addEventListener('change', showSelectedFlow);
    showSelectedFlow();
}

export  function showSelectedFlow() : string{
    const selectedIndex = selectFlow.selectedIndex;
    
    if (selectedIndex !== -1) {
        const selectedOption = selectFlow.options[selectedIndex];
        return selectedOption.text; //gekozen flow
        
    }return "";
}
export function showSelectedQuestion() : string{
    const selectedIndex = selectQuestion.selectedIndex;

    if (selectedIndex !== -1) {
        const selectedOption = selectQuestion.options[selectedIndex];
        return selectedOption.value.toString(); //gekozen question
    }return "0";
}
export function showSelectedQuestionText() : string{
    const selectedIndex = selectQuestion.selectedIndex;

    if (selectedIndex !== -1) {
        const selectedOption = selectQuestion.options[selectedIndex];
        return selectedOption.text //gekozen question
    }return "0";
}


export function initDataStatistics(labels: string[]) {
    GetCountStepsPerFlow(labels)
    GetCountParticipationsPerFlow(labels)
    fillDropdownFlows(labels, selectFlow);

    //eerste keer bij inladen
    GetQuestionsFromFlow(showSelectedFlow());
    GetQuestionNames(showSelectedFlow());
    GetParticipatoinNames(showSelectedFlow());

    selectFlow.addEventListener('change', () => {
        GetQuestionsFromFlow(showSelectedFlow());
        GetQuestionNames(showSelectedFlow());
        GetParticipatoinNames(showSelectedFlow());
    });
}
export function initQuestionNames(labels: Question[]) {
    fillDropdownQuestions(labels,selectQuestion)
    GetChoicesNames(showSelectedQuestion())
}
export function initChoicesNames(labels: string[]) {
    GetAnswerCountsForQuestions(labels,showSelectedQuestion(),showSelectedQuestionText());
}

GetNamesPerFlow()
