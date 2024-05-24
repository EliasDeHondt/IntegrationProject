import {CreateIdea} from "./WebAppAPI";
import {Idea} from "../Types/WebApp/Types";
import {readFileAsBase64} from "../Util";

export async function PostIdea(feedId: number, text: string): Promise<Idea>{
    const fileInput = document.getElementById("file-input") as HTMLInputElement;

    let base64: string | null;
    if(fileInput.files![0] != null){
        let image = fileInput.files![0];
        base64 = await readFileAsBase64(image);
    } else {
        base64 = null;
    }
    
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