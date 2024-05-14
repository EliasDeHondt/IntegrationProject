export async function createPlatform(organisationName: string, logo?: File) {

    let base64String = null;
    
    if (logo) {
        base64String = await readFileAsBase64(logo);
    }
    
    return await fetch ("/api/SharedPlatforms", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            organisationName: organisationName,
            logo: base64String
        })
    })
}

async function readFileAsBase64(file: File): Promise<string | null> {
    return new Promise((resolve) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            const base64String = reader.result?.toString().split(',')[1];
            resolve(base64String ?? null);
        };
    });
}
