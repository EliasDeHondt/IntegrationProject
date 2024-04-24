export async function deleteUser(email: string) {
    await fetch("/api/Users/DeleteUser/" + email, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json"
        }
    })
}