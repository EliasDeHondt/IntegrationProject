import {Modal} from "bootstrap";
import Chart from 'chart.js/auto';

function chartData(label: string, labels: string[], data: string[]) {
    // Data for the chart
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

// Get the canvas elements
const barCtx = document.getElementById('barChart') as HTMLCanvasElement;
const lineCtx = document.getElementById('lineChart') as HTMLCanvasElement;
const doughnutCtx = document.getElementById('doughnutChart') as HTMLCanvasElement;
const radarCtx = document.getElementById('radarChart') as HTMLCanvasElement;

// Create chart data
const barChartData = chartData('Step types', ["Single", "Multiple", "Open", "Info", "Link"], ["2", "6", "4", "5", "3"]);

// Create the bar chart
const barChart = new Chart(barCtx, {
    type: 'bar',
    data: barChartData,
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        },
        plugins: {
            title: {
                display: true,
                text: 'Bar Chart Title'
            }
        }
    }
});


var lineChart = new Chart(lineCtx, {
    type: 'line',
    data: barChartData,
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});
var doughnutChart = new Chart(doughnutCtx, {
    type: 'doughnut',
    data: barChartData,
    options: {}
});

var radarChart = new Chart(radarCtx, {
    type: 'radar',
    data: barChartData,
    options: {}
});

