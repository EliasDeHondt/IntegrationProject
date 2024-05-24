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

export async function generateQrCode(text: string): Promise<string> {
    return await fetch('/api/Qr', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ 
            text: text
        })
    })
        .then(response => response.text())
        .then(data => {
            return data
        })
}