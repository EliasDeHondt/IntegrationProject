export async function downloadVideoFromBucket(videoName: string): Promise<string | void> {
    return await fetch("/api/Storage/DownloadVideo/" + videoName, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.text())
        .then(data => {
            return data;
        })
        .catch(Error => console.log(Error))
}

export async function uploadVideoToBucket(file: File): Promise<string> {
    const formData = new FormData();
    formData.append("file", file);
    return await fetch('/api/Storage/UploadVideo', {
        method: "POST",
        body: formData
    }).then(response => {
        return response.text();
    })
}