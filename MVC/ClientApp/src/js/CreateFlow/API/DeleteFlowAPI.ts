export async function deleteFlow(id: number) {
    console.log("Deleting flow with ID: " + id + "...")
    
    try {
        let response = await fetch("CreateFlow/DeleteFlow/" + id, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json"
            }
        });
        if (response.ok) {
            console.log(`Flow ${id} deleted successfully.`);
        } else {
            console.error(`Failed to delete flow ${id}.`);
        }
    } catch (error) {
        console.error("Error:", error);
    }
}