export async function getProjectRespondentsCount(projectId: number) : Promise<number> {
    return await fetch(`/api/Statistics/GetRespondentCountFromProject/${projectId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
}

export async function getProjectFlowsCount(projectId: number) : Promise<number> {
    return await fetch(`/api/Statistics/GetFlowCountFromProject/${projectId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
}

export async function getProjectSubThemesCount(projectId: number) : Promise<number> {
    return await fetch(`/api/Statistics/GetSubThemeCountFromProject/${projectId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
}