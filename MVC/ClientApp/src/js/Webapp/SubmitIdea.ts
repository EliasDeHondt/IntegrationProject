import {CreateIdea} from "./WebAppAPI";
import {Idea} from "../Types/WebApp/Types";

export async function PostIdea(feedId: number): Promise<Idea>{
    const textArea = document.getElementById("textIdea") as HTMLTextAreaElement;
    let idea: Idea = {
        id: 0,
        text: textArea.value,
        author: {
            email: "",
            name: ""
        },
        likes: []
    }
    textArea.value = "";
    return await CreateIdea(idea, feedId);
}