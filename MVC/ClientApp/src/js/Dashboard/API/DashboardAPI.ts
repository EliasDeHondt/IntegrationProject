import {User} from "../Types/UserTypes";


export async function getUsersForPlatform(platformId: string): Promise<User[]>{
    return await fetch("/api/Users/GetUsersForPlatform/" + platformId)
        .then(response => response.json())
        .then(data => {
            return data
        })
}

export function generateCard(user: User): string {
    return `
        <div class="col mt-3 mb-3">
            <div class="card border-black border-2 bgAccent h-100">
                <div class="card-body">
                    <p class="text-white">Name: ${user.userName}</p>
                    <p class="text-white">Email: ${user.email}</p>
                </div>
            </div>
        </div>
    `;
}