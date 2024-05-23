import {CreateIdea} from "./WebAppAPI";
import {Idea} from "../Types/WebApp/Types";

export async function PostIdea(feedId: number, text: string): Promise<Idea>{
    
    let idea: Idea = {
        id: 0,
        text: text,
        author: {
            email: "",
            name: ""
        },
        likes: []
    }
    return await CreateIdea(idea, feedId);
}