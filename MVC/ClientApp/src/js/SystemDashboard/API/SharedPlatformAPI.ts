import {SharedPlatform} from "../../Types/PlatformTypes";

export async function GetSharedPlatforms(): Promise<SharedPlatform[]> {
    return await fetch("/api/SharedPlatforms", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    })
        .then(r => r.json())
        .then(data => {
        return data;
    });
}