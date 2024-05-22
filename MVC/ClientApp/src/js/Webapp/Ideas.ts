import {CreateIdea} from "./WebAppAPI";
import {Idea} from "../Types/WebApp/Types";
import {readFileAsBase64} from "../Util";

export async function PostIdea(feedId: number): Promise<Idea>{
    const textArea = document.getElementById("textIdea") as HTMLTextAreaElement;
    const fileInput = document.getElementById("file-input") as HTMLInputElement;

    let image = fileInput.files![0];
    let base64 = await readFileAsBase64(image);
    
    let idea: Idea = {
        id: 0,
        text: textArea.value,
        author: {
            email: "",
            name: ""
        },
        likes: [],
        image: base64
    }
    textArea.value = "";
    return await CreateIdea(idea, feedId);
}