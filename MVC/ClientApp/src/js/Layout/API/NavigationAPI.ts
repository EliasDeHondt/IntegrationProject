// Nav
import {getLoggedInEmail, getUsersForPlatform} from "../../Dashboard/API/DashboardAPI";

export function showUserName(id: string, accountname: HTMLElement){
    getUsersForPlatform(id)
        .then(users => {
            getLoggedInEmail()
                .then(email => {
                    users.forEach(user => {
                        if (user.email === email) {
                            accountname.textContent = "Welcome " + user.userName + "!";
                        }
                    });
                })
                .catch(error => {
                    console.error('Error getting logged-in email:', error);
                });
        })
        .catch(error => {
            console.error('Error getting users for platform:', error);
        });
}