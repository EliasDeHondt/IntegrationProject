import { Chart } from "chart.js";


export function chartDataToCSV(label: string,chart: Chart<"line" | "bar" | "radar" | "doughnut", string[], string> | null) {
    if(chart != null){
        const labels = chart.data.labels;
        const datasets = chart.data.datasets;

        let csv = label+',' + datasets.map(ds => ds.label).join(',') + '\n';

        // @ts-ignore
        labels.forEach((label, index) => {
            csv += label;
            datasets.forEach((dataset) => {
                csv += ',' + dataset.data[index];
            });
            csv += '\n';
        });
        return csv;
    }return "";
}
export function chartDatasToCSV(label: string,chart1: Chart<"line" | "bar" | "radar" | "pie",string[], string> | null,chart2: Chart<"line" | "bar" | "radar" | "pie",string[], string> | null) {
    if (chart1 !== null && chart2 !== null) {
        const labels = chart1.data.labels;
        const datasets1 = chart1.data.datasets;
        const datasets2 = chart2.data.datasets;

        let csv = label + ',' + datasets1.map(ds => ds.label).join(',') + ',' + datasets2.map(ds => ds.label).join(',') + '\n';

        // @ts-ignore
        labels.forEach((label, index) => {
            csv += label;
            datasets1.forEach((dataset) => {
                csv += ',' + dataset.data[index];
            });
            datasets2.forEach((dataset) => {
                csv += ',' + dataset.data[index];
            });
            csv += '\n';
        });
        return csv;
    }return "";
}
export function downloadCSV(csv: string, filename: string) {
    const blob = new Blob([csv], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.setAttribute('hidden', '');
    a.setAttribute('href', url);
    a.setAttribute('download', filename);
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
}

