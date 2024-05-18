import {Feed} from "../Types/WebApp/Types";

export async function GetFeed(Id: number): Promise<Feed> {
    return await fetch(`/api/Feeds/${Id}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }).then(response => response.json()).then(data => {
        return data;
    })
}

export async function GetRandomFeed(): Promise<Feed> {
    return await fetch('/api/Feeds/random', {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            return data
        })
}