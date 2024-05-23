export function delay(ms: number){
    return new Promise( resolve => setTimeout(resolve, ms))
}

export async function readFileAsBase64(file: File): Promise<string | null> {
    return new Promise((resolve) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            const base64String = reader.result?.toString().split(',')[1];
            resolve(base64String || null);
        };
    });
}