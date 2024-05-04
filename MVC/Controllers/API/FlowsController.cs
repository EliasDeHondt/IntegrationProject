using Business_Layer;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class FlowsController : Controller
{
    private readonly FlowManager _manager;
    private readonly IHubContext<FacilitatorHub> _hub;

    public FlowsController(FlowManager manager, IHubContext<FacilitatorHub> hubContext)
    {
        _manager = manager;
        _hub = hubContext;
    }

    [HttpPost("SetRespondentEmail/{flowId:int}/{inputEmail}")]
    public IActionResult SetRespondentEmail(int flowId, string inputEmail)
    {
        _manager.SetParticipationByFlow(flowId, inputEmail);

        return Ok();
    }

    [HttpPut("{id}/{state}")]
    public IActionResult PutFlowState(long id, string state)
    {
        Flow flow = _manager.GetFlowByIdWithTheme(id);

        if (flow == null)
            return NotFound();

        if (Enum.TryParse(state, out FlowState flowState))
            flow.State = flowState;
        _manager.ChangeFlowState(flow);
        
        return NoContent();
    }
    
    [HttpGet]
    public ActionResult GetFlows()
    {
        var flows = _manager.GetAllFlowsWithTheme();

        if (!flows.Any())
            return NoContent();

        return Ok(flows.Select(flow => new FlowViewModel
        {
            Id = flow.Id,
            FlowType = flow.FlowType,
            Steps = flow.Steps,
            Participations = flow.Participations,
            ThemeId = flow.Theme.Id
        }));
    }
}