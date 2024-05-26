import * as signalR from "@microsoft/signalr"

class SignalRConnectionManager {
    private static instance: signalR.HubConnection;

    private constructor() {
    }

    public static getInstance(): signalR.HubConnection {
        if (!SignalRConnectionManager.instance) {
            const URL = window.location.hostname == "localhost"
                ? "http://localhost:5247/hub"
                : "https://codeforge.eliasdh.com/hub"
            
            SignalRConnectionManager.instance = new signalR.HubConnectionBuilder()
                .withUrl(URL)
                .withAutomaticReconnect([0, 2000, 10000, 30000])
                .build();
            
            SignalRConnectionManager.instance.keepAliveIntervalInMilliseconds = 150000;
            SignalRConnectionManager.instance.serverTimeoutInMilliseconds = 60000;
        }
        return SignalRConnectionManager.instance;
    }
    
    public static async startConnection(): Promise<void> {
        const connection = SignalRConnectionManager.getInstance();
        if (connection.state == signalR.HubConnectionState.Disconnected) {
            await connection.start();
        }
    }
}

export default SignalRConnectionManager;