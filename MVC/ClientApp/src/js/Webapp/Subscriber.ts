
export async function subscribe(feedId: number){
    console.log("subscribing");
    await fetch("/api/Feeds/subscribe/" + feedId, { method: "POST" });
}

export async function unsubscribe(feedId: number){
    console.log("unsubscribing")
    await fetch("/api/Feeds/unsubscribe/" + feedId, { method: "DELETE" });
}