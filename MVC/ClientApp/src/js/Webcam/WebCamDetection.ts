import * as tf from "@tensorflow/tfjs";
import * as cocoSsd from '@tensorflow-models/coco-ssd';

console.log(tf.version)

const FPS: number = 30;
const height: number = 480;
const width: number = 640;

let video: HTMLVideoElement = document.getElementById("vidInput") as HTMLVideoElement;
let ct: HTMLCanvasElement = document.getElementById("canvasTransfer") as HTMLCanvasElement;
ct.width = video.videoWidth;
ct.height = video.videoHeight;
let canvas: HTMLCanvasElement = document.getElementById("canvasCV") as HTMLCanvasElement;
let context: CanvasRenderingContext2D | null = ct.getContext("2d");

video.disablePictureInPicture = true;

async function main() {
    const model = await cocoSsd.load();
    navigator.mediaDevices.getUserMedia({video: true, audio: false})
        .then(stream => {
            video.srcObject = stream;
            video.onloadedmetadata = () => {
                video.play();
                detectPeople();
            }
        })
        .catch(function (err) {
            console.log("An error occurred! " + err);
        });

    async function detectPeople() {
        const predictions = await model.detect(video);
        context?.clearRect(0, 0, canvas.width, canvas.height);

        predictions.forEach(prediction => {
            if (prediction.class === "person") {
                const box = prediction.bbox;
                context?.strokeRect(box[0], box[1], box[2], box[3]);
            }
        });
        requestAnimationFrame(detectPeople);
    }
}

main()