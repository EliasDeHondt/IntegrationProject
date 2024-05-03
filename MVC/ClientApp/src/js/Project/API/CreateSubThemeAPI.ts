export async function createSubTheme(subject: string, mainThemeId: number) {
    return fetch(`/api/SubThemes/AddSubTheme`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            subject: subject, 
            mainThemeId: mainThemeId}),
    })
}