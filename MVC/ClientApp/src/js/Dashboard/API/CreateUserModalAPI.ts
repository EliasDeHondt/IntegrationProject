export async function createFacilitator(name: string, email: string, password: string, projectIds: number[], platform: string) {
    await fetch("/api/Users/CreateFacilitator", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: name,
            email: email,
            password: password,
            projectIds: projectIds,
            platformId: platform
        })
    })
}

export async function createAdmin(name: string, email:string, password: string, platform: string, permissions: string[]) {
    await fetch("/api/Users/CreateAdmin", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: name,
            email: email,
            password: password,
            platformId: platform,
            permissions: permissions
        })
    })
}

export async function getProjects(id: string) {
    return await fetch(`/api/Projects/GetProjectsForSharedPlatform/${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {return data})
}

