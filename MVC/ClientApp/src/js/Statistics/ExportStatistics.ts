import { Chart } from "chart.js";


export function chartDataToCSV(chart: Chart<"radar", string[], string> | null) {
    if(chart != null){
        const labels = chart.data.labels;
        const datasets = chart.data.datasets;

        let csv = 'Label,' + datasets.map(ds => ds.label).join(',') + '\n';

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

