export async function isEmailInUse(email: string): Promise<boolean> {
    let result = true;
    await fetch("/api/Users/IsEmailInUse", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            email: email
        })
    })
        .then(response => response.json())
        .then(data => {
            result = data;
        })
        .catch(error => {
            console.error('Error:', error);
        })
    return result;
}

export async function isUserInRole(role: string): Promise<boolean> {
    let result = false;
    
    await fetch(`/api/Users/IsUserInRole/${role}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            result = data;
        })
        .catch(error => {
            console.error('Error:', error)
        })
    return result;
}