export async function createProject(name: string, description: string, platform: string, logo?: File) {
    let bodyData: { // Indexsignatuur ala Elias ;)
        name: string;
        description: string;
        sharedplatformId: string;
        [key: string]: string;
    } = {
        name: name,
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

async function readFileAsBase64(file: File): Promise<string | null> {
    return new Promise((resolve) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            const base64String = reader.result?.toString().split(',')[1];
            resolve(base64String || null);
        };
    });
}