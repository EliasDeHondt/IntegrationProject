using Microsoft.AspNetCore.SignalR;

namespace Domain.FacilitatorFunctionality;

public class FacilitatorHub : Hub
{
    public async Task SendFlowState(string id, string state) =>
        await Clients.All.SendAsync("ReceiveFlowState", id, state);
}