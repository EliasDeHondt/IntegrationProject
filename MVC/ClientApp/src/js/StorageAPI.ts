
export async function downloadVideoFromBucket(videoName: string): Promise<string | void>{
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