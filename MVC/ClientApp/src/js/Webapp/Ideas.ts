import {CreateIdea} from "./WebAppAPI";
import {Idea} from "../Types/WebApp/Types";
import {readFileAsBase64} from "../Util";

export async function PostIdea(feedId: number, text: string): Promise<Idea>{
    const textArea = document.getElementById("textIdea") as HTMLTextAreaElement;
    const fileInput = document.getElementById("file-input") as HTMLInputElement;

    let image = fileInput.files![0];
    let base64 = await readFileAsBase64(image);
    
    let idea: Idea = {
        id: 0,
        text: text,
        author: {
            email: "",
            name: ""
        },
        likes: [],
        image: base64
    }
    return await CreateIdea(idea, feedId);
}