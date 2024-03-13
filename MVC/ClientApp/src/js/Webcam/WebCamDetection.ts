import cv from '@techstark/opencv-js';

const FPS: number = 30;
const height: number = 480;
const width: number = 640;

console.log(cv)
console.log(cv.getVersionMajor())

let video: HTMLVideoElement = document.getElementById("vidInput") as HTMLVideoElement;
let ct: HTMLCanvasElement = document.getElementById("canvasTransfer") as HTMLCanvasElement;
let canvas: HTMLCanvasElement = document.getElementById("canvasCV") as HTMLCanvasElement;
let context: CanvasRenderingContext2D | null = ct.getContext("2d");
let src: cv.Mat = new cv.Mat(height, width)
let dst: cv.Mat = new cv.Mat(height, width)
let capture: cv.VideoCapture = new cv.VideoCapture(video);


video.disablePictureInPicture = true;


navigator.mediaDevices.getUserMedia({video: true, audio: false})
    .then(stream => processVideo())
    .catch(function (err) {
        console.log("An error occurred! " + err);
    });


function processVideo(): void {
    let begin = Date.now();
    capture.read(src)
    cv.cvtColor(src, dst, cv.COLOR_RGBA2GRAY);
    cv.imshow(canvas, dst);
    let delay = 1000/FPS - (Date.now() - begin);
    setTimeout(processVideo, delay);
}

setTimeout(processVideo, 0)