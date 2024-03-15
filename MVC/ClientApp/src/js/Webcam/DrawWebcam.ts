function drawWebcam(ctxVideo: CanvasRenderingContext2D, video: HTMLVideoElement, detectionCanvas: HTMLCanvasElement){
    while(true){
        ctxVideo.drawImage(video, 0, 0, detectionCanvas.width, detectionCanvas.height);
    }
}