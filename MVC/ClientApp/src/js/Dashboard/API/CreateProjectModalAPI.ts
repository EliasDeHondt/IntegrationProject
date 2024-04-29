export async function createProject(name: string, description: string, platform: string, logo?: File) {
    const formData = new FormData();
    formData.append('name', name);
    formData.append('description', description);
    formData.append('sharedplatformId', platform);
    
    if (logo) { // If logo is present, convert it to base64 string and add it to formData
        const base64String = await readFileAsBase64(logo);
        if (base64String) {
            formData.append('logoBase64', base64String);
        }

        console.log('Base64 image string:', base64String);
        console.log('Type:', typeof base64String);
    }
    
    await fetch("/api/Projects/AddProject", {
        method: "POST",
        body: formData
    });
}

async function readFileAsBase64(file: File): Promise<string | null> { // Convert file to base64 string
    return new Promise((resolve) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            const base64String = reader.result?.toString().split(',')[1];
            resolve(base64String || null);
        };
    });
}