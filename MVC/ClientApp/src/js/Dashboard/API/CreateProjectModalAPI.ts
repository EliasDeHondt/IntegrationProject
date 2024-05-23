import {readFileAsBase64} from "../../Util";

export async function createProject(name: string, description: string, platform: string, logo?: File) {
    let bodyData: { // Indexsignatuur ala Elias ;)
        title: string;
        description: string;
        sharedplatformId: string;
        [key: string]: string;
    } = {
        title: name,
        description: description,
        sharedplatformId: platform
    };

    if (logo) {
        const base64String = await readFileAsBase64(logo);
        if (base64String) bodyData["Image"] = base64String;
    }

    await fetch("/api/Projects/AddProject", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(bodyData)
    });
}