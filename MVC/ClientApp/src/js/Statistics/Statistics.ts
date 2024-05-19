import Chart from 'chart.js/auto';

const barCtx = document.getElementById('barChart') as HTMLCanvasElement;
const lineCtx = document.getElementById('lineChart') as HTMLCanvasElement;
const doughnutCtx = document.getElementById('doughnutChart') as HTMLCanvasElement;
const radarCtx = document.getElementById('radarChart') as HTMLCanvasElement;

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
let doughnutChart: Chart<"doughnut", string[], string> | null = null;
function drawDoughnutChart(titel:string,labels: string[], label: string, data: string[]){
    // @ts-ignore
    if (doughnutChart) {
        doughnutChart.destroy();
    }
    
    doughnutChart = new Chart(doughnutCtx, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                label: label,
                data: data,
                backgroundColor: colors,
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }],
            
        },
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
                },
                legend: {
                    labels: {
                        font: {
                            size: 22
                        }
                    }
                },
                tooltip: {
                    bodyFont: {
                        size: 34 
                    }
                }

            }
        }
    });
}
let radarChart: Chart<"radar", string[], string> | null = null;

function drawRadarChart(titel:string,chartData: { labels: string[]; datasets: { label: string; data: string[]; backgroundColor: string; borderColor: string; borderWidth: number; }[]; }){

    if (radarChart) {
        radarChart.destroy();
    }
    
     radarChart = new Chart(radarCtx, {
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
                },
                // x: {
                //     beginAtZero: true,
                //     ticks: {
                //         font: {
                //             size: 22
                //         }
                //     }
                // }
            },
            plugins: {
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
                            size: 24 
                        }
                    }
                }

            }
        }
    });
}

//get data - flows
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
// function drawCharts(labels: string[],data: string[]){
//     drawBarChart("Aantal Steps per Flow",chartData('Aantal Steps', labels, data))
//     drawLineChart("Aantal Steps",chartData('Step types', labels, data))
//     drawDoughnutChart("Aantal Steps",chartData('Step types', labels, data))
//     drawRadarChart("Aantal Steps",chartData('Step types', labels, data))
// }
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
            fillDropdownFlows(labels, selectFlow);

            GetQuestionsFromFlow(showSelectedFlow()); //eerste keer bij inladen
            GetQuestionNames(showSelectedFlow()); //eerste keer bij inladen
            
            selectFlow.addEventListener('change', () => {
                GetQuestionsFromFlow(showSelectedFlow());
                GetQuestionNames(showSelectedFlow());
            });
        } )
        .catch(error => console.error("Error:", error))
}
// export async function GetAnswersPerQuestion(question: string){
//     //voor answers te krijgen
//     console.log("Fetching Answers...")
//     await fetch("/api/Statistics/GetAnswersPerQuestion", {
//         method: "GET",
//         headers: {
//             "Accept": "application/json",
//             "Content-Type": "application/json"
//         }
//     })
//         .then(response => response.json())
//         .then(labels => {
//             console.log(labels)
//             fillDropdownFlows(labels,selectQuestion);
//             showSelectedFlow();
//             GetAnswerCountsForQuestions(labels,question)
//         } )
//         .catch(error => console.error("Error:", error))
// }
export async function GetQuestionNames(flowname: string){
    console.log("Fetching question names...")
    await fetch("/api/Statistics/GetQuestionNames/" + flowname, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(labels => {
            console.log(labels)
            fillDropdownFlows(labels,selectQuestion);
            GetAnswerCountsForQuestions(labels,showSelectedQuestion());
        } )
        .catch(error => console.error("Error:", error))
}

//get data - flow
export async function GetQuestionsFromFlow(flowname: string){
    console.log("Fetching count questions...")
    const labels: string[] = ["MultipleChoiceQuestion", "SingleChoiceQuestion","OpenQuestion","RangeQuestion"];
    await fetch("/api/Statistics/GetQuestionsFromFlow/" + flowname, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => drawRadarChart("Aantal Questions voor de Flow: "+flowname,chartData('Aantal Questions', labels, data)))
        .catch(error => console.error("Error:", error))
}

export async function GetAnswerCountsForQuestions(labels: string[],question: string){
    //voor elk answer te tellen
    console.log("Fetching count answers...")
    
    // await fetch("/api/Statistics/GetAnswerCountsForQuestions/" + question, {
    //     method: "GET",
    //     headers: {
    //         "Accept": "application/json",
    //         "Content-Type": "application/json"
    //     }
    // })
    //     .then(response => response.json())
    //     .then(data => drawDoughnutChart("Aantal antwoorden voor de vraag: "+question,chartData('Aantal Answers', labels, data)))
    //     .catch(error => console.error("Error:", error))
    const data: string[] = ["4", "3","8","1"];
    drawDoughnutChart("Aantal antwoorden voor de vraag: "+question, labels,'Aantal Answers', data)
}

function getSelectedFlows(): number[] {
    let flowIds: number[] = [];
    for (let i = 0; i < selectFlow.options.length; i++) {
        if (selectFlow.options[i].selected) {
            flowIds.push(Number(selectFlow.options[i].value));
        }
    }
    console.log("flowIds: ", flowIds)
    return flowIds
}
function fillDropdownFlows(data: string[], select: HTMLSelectElement) {
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

function showSelectedFlow() : string{
    const selectedIndex = selectFlow.selectedIndex;
    
    if (selectedIndex !== -1) {
        const selectedOption = selectFlow.options[selectedIndex];
        return selectedOption.text; //gekozen flow
        
    }return "";
}
function showSelectedQuestion() : string{
    const selectedIndex = selectQuestion.selectedIndex;

    if (selectedIndex !== -1) {
        const selectedOption = selectQuestion.options[selectedIndex];
        return selectedOption.text; //gekozen question
    }return "";
}





// getSelectedFlows()

// document.addEventListener('DOMContentLoaded', (event) => {
//     GetNamesPerFlow()
//     console.log("showSelectedFlow() ", showSelectedFlow());
//     GetQuestionsFromFlow(showSelectedFlow())
// });
GetNamesPerFlow()

// console.log("showSelectedFlow()1 ", showSelectedFlow());
//GetAnswersPerQuestion("What would help you make a choice between the different parties?")