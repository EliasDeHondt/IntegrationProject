export async function createProject(name: string, description: string, platform: string, logo?: File) {
    const formData = new FormData();
    formData.append('name', name);
    formData.append('description', description);
    formData.append('sharedplatformId', platform);

    // If logo is present, convert it to base64 string and add it to formData
    if (logo) {
        const reader = new FileReader();
        reader.readAsDataURL(logo);
        reader.onload = async function () {
            const base64String = reader.result?.toString().split(',')[1];
            if (base64String) {
                formData.append('logoBase64', base64String);
            }

            // Submit the form with the base64 string of the image
            await fetch("/api/Projects/AddProject", {
                method: "POST",
                body: formData
            });
        };
    } else {
        // If logo is not present, send formData without logo
        await fetch("/api/Projects/AddProject", {
            method: "POST",
            body: formData
        });
    }
}