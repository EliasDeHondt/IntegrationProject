import * as tf from "@tensorflow/tfjs";
import * as cocoSsd from '@tensorflow-models/coco-ssd';

console.log(tf.version);
const FPS: number = 30;
const height: number = 480;
const width: number = 640;

let video: HTMLVideoElement = document.getElementById("vidInput") as HTMLVideoElement;
let ct: HTMLCanvasElement = document.getElementById("canvasTransfer") as HTMLCanvasElement;
ct.width = video.videoWidth;
ct.height = video.videoHeight;
let canvas: HTMLCanvasElement = document.getElementById("canvasCV") as HTMLCanvasElement;
let context: CanvasRenderingContext2D | null = ct.getContext("2d");
let liveview: HTMLDivElement = document.getElementById("liveview") as HTMLDivElement;

video.disablePictureInPicture = true;

const model = await cocoSsd.load();
navigator.mediaDevices.getUserMedia({video: true, audio: false})
    .then(stream => {
        video.srcObject = stream;
        video.onloadedmetadata = () => {
            video.play().then(() => {
                ct.width = video.videoWidth;
                ct.height = video.videoHeight;
            });
            predictWebcam();
        }
    })
    .catch(function (err) {
        console.log("An error occurred! " + err);
    });

let children:any[] = [];

function predictWebcam(){
    context?.drawImage(video, 0, 0, ct.width, ct.height);
    model.detect(video).then(predictions => {
        context?.clearRect(0, 0, ct.width, ct.height);
        for(let i = 0; i < children.length; i++){
            liveview.removeChild(children[i])
        }
        children.splice(0);
        predictions.forEach(prediction => {
            if(prediction.score > 0.66 && prediction.class === "person"){
                /*const highlighter = document.createElement('div');
                highlighter.setAttribute('class','highlighter');
                highlighter.style.left = prediction.bbox[0] + 'px';
                highlighter.style.top = prediction.bbox[1] + 'px';
                highlighter.style.width = prediction.bbox[2] + 'px';
                highlighter.style.height = prediction.bbox[3] + 'px';
                
                liveview.appendChild(highlighter);
                children.push(highlighter);*/
                let box = prediction.bbox;
                context?.strokeRect(box[0], box[1], box[2], box[3]);
            }
        })
        window.requestAnimationFrame(predictWebcam);
    })
}