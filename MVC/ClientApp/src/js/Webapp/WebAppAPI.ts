import {Feed, Idea, Like, Reaction} from "../Types/WebApp/Types";
import {User} from "../Types/UserTypes";

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

export async function GetFeedIds(): Promise<Feed[]> {
    return await fetch('/api/Feeds/ids', {
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

export async function CreateIdea(idea: Idea, feedId: number): Promise<Idea> {
    return await fetch(`/api/Ideas/createIdea/${feedId}`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(idea)
    })
        .then(response => response.json())
        .then(data => {
            return data
        })
}

export async function CreateLike(ideaId: number): Promise<Like>{
    return await fetch(`/api/Ideas/likeIdea/${ideaId}`, {
        method: 'POST',
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

export async function DeleteLike(ideaId: number): Promise<void>{
    await fetch(`/api/Ideas/unlikeIdea/${ideaId}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
}

export async function GetLoggedInUser(): Promise<User>{
    return await fetch('/api/Users/GetLoggedInUser', {
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

export async function GetReactions(ideaId: number): Promise<Reaction[]> {
    return await fetch(`/api/Reactions/${ideaId}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
}

export async function PostReaction(ideaId: number, text: string): Promise<Reaction> {
    return await fetch(`/api/Reactions`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            ideaId: ideaId,
            text: text
        })
    })
        .then(response => response.json())
        .then(data => {
            return data
        })
}