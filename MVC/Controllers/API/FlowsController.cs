using Business_Layer;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class FlowsController : Controller
{
    private readonly FlowManager _manager;

    public FlowsController(FlowManager manager)
    {
        _manager = manager;
    }

    [HttpPost("SetRespondentEmail/{flowId:int}/{inputEmail}")]
    public IActionResult SetRespondentEmail(int flowId, string inputEmail)
    {
        _manager.SetParticipationByFlow(flowId, inputEmail);

        return Ok();
    }

    [HttpPut("{id}/Paused")]
    public IActionResult PutFlowStateToPaused(long id)
    {
        Flow flow = _manager.GetFlowByIdWithTheme(id);

        if (flow == null)
            return NotFound();

        flow.State = FlowState.Paused;
        _manager.ChangeFlowState(flow);
        
        return NoContent();
    }

    [HttpPut("{id}/Active")]
    public IActionResult PutFlowStateToActive(long id)
    {
        Flow flow = _manager.GetFlowByIdWithTheme(id);

        if (flow == null)
            return NotFound();

        flow.State = FlowState.Active;
        _manager.ChangeFlowState(flow);
        
        return NoContent();
    }
}