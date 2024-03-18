export default function runAsync(fn: Function) {
    const worker = new Worker(
        URL.createObjectURL(new Blob([`postMessage((${fn})());`])
    ));
    return new Promise((res, rej) => {
        worker.onmessage = ({ data }) => {
            res(data)
            worker.terminate();
        };
        worker.onerror = err => {
            rej(err)
            worker.terminate();
        };
    });
}