import * as tf from "@tensorflow/tfjs";
import * as cocoSsd from '@tensorflow-models/coco-ssd';
import {ChoiceBox} from "./WebCamUtil";

const video: HTMLVideoElement = document.getElementById("vidInput") as HTMLVideoElement;
const detectionCanvas: HTMLCanvasElement = document.getElementById("canvasDetection") as HTMLCanvasElement;
const choiceCanvas: HTMLCanvasElement = document.getElementById("canvasChoices") as HTMLCanvasElement;
const ctx: CanvasRenderingContext2D= detectionCanvas.getContext("2d") ?? new CanvasRenderingContext2D();
const ctxChoice: CanvasRenderingContext2D= choiceCanvas.getContext("2d") ?? new CanvasRenderingContext2D();
const choiceBoxes: ChoiceBox[] = [];

video.disablePictureInPicture = true;


export async function loadModel(): Promise<cocoSsd.ObjectDetection> {
    return await cocoSsd.load();
}

export async function startPhysical(model: cocoSsd.ObjectDetection){
    await tf.setBackend('webgl');
    navigator.mediaDevices.getUserMedia({video: true, audio: false})
        .then(stream => {
            video.srcObject = stream;
            video.onloadedmetadata = () => {
                video.play().then(() => {
                    detectionCanvas.width = video.videoWidth;
                    detectionCanvas.height = video.videoHeight;
                    choiceCanvas.width = video.videoWidth;
                    choiceCanvas.height = video.videoHeight;
                    predictWebcam();
                });
                
            }
        })
        .catch(function (err) {
            console.log("An error occurred! " + err);
        });

    function predictWebcam(){
        model.detect(video).then(predictions => {
            ctx.clearRect(0, 0, detectionCanvas.width, detectionCanvas.height);
            predictions.forEach(prediction => {
                if(prediction.score > 0.66 && prediction.class === "person"){
                    let box = prediction.bbox;
                    ctx.strokeStyle = 'red';
                    ctx.strokeRect(box[0], box[1], box[2], box[3]);
                    for(let i:number = 0; i < choiceBoxes.length; i++){
                        if(choiceBoxes[i].isInside(box[0] + (box[2]/2))) console.log("Inside of box " + (i+1));
                    }
                }
            })

            window.requestAnimationFrame(predictWebcam);
        })
    }
    
}




export function drawChoiceBoundaries(choices: number, width: number, height: number){
    let lineDiff: number = width/choices;
    const rectWidth: number = 10;
    ctxChoice.fillStyle = 'green';
    for(let i: number = 0; i < choices; i++){
        choiceBoxes[i] = new ChoiceBox(lineDiff * i, lineDiff * (i+1));
        if(i > 0) ctxChoice.fillRect((lineDiff * i) - (rectWidth/2), 0, rectWidth, height);
    }
}