export async function createProject(name: string, description:string, platform: string) {
    await fetch("/api/Projects/AddProject", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            title: name,
            description: description,
            sharedplatformId: platform
        })
    })
}