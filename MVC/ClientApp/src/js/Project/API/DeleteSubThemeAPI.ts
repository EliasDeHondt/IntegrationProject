export async function deleteSubTheme(subThemeId: number) {
    console.log("Deleting SubTheme with ID: " + subThemeId);
    await fetch(`/api/SubThemes/DeleteSubTheme/` + subThemeId, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        }
    });
}