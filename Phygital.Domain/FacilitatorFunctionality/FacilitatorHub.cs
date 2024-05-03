using Domain.ProjectLogics;
using Microsoft.AspNetCore.SignalR;

namespace Domain.FacilitatorFunctionality;

public class FacilitatorHub : Hub
{
    public async Task ConnectToUser(string code) =>
        await Groups.AddToGroupAsync(Context.ConnectionId, code);
    
    public async Task SendFlowUpdate(string code, string id, string state) =>
        await Clients.OthersInGroup(code).SendAsync("ReceiveFlowUpdate", id, state);
}