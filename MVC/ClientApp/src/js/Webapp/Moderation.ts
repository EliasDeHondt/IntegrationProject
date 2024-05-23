const response = async (ideaText: string): Promise<boolean> => {
    let response =  await fetch('/api/Ai/CheckIdeaText', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            text: ideaText
        })
    })
        .then(response => response.json())
        .then(data => {
            return data
        }) as string;
    return !(response.includes("Toxicity") || response.includes("Hate Speech") || response.includes("Threats"));
}

export async function IsValidIdea(ideaText: string): Promise<boolean> {
    return response(ideaText).then(res => {
        console.log(res);
        return res;
    })
}