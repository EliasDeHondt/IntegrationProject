using System.Collections;
using Business_Layer;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Microsoft.AspNetCore.SignalR;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class FlowsController : Controller
{
    private readonly FlowManager _manager;
    private readonly UnitOfWork _uow;
    private readonly IHubContext<FacilitatorHub> _hub;

    public FlowsController(FlowManager manager, UnitOfWork uow, IHubContext<FacilitatorHub> hubContext)
    {
        _manager = manager;
        _uow = uow;
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

    [HttpGet("{id}")]
    public IActionResult GetFlow(long id)
    {
        var flow = _manager.GetFlowById(id);

        if (flow == null)
            return NotFound();

        return Ok(new FlowViewModel
        {
            FlowType = flow.FlowType,
            Id = flow.Id,
            Participations = flow.Participations,
            Steps = flow.Steps
        });
    }

    [HttpPut("/{flowId}/Update")]
    public IActionResult UpdateFlow(long flowId, [FromBody] FlowViewModel model)
    {
        var flow = _manager.GetFlowById(flowId);
        
        _uow.BeginTransaction();
        flow.Steps = model.Steps.ToList();
        _manager.UpdateFlow(flow);
        _uow.Commit();

        return NoContent();
    }
    
    [HttpGet]
    public ActionResult GetFlows()
    {
        var flows = _manager.GetAllFlows();

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
    
    [HttpGet("{type}")]
    public ActionResult GetFlowsByType(string type)
    {
        var flows = _manager.GetAllFlowsByType(type);

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
    
    [HttpGet("{id:long}")]
    public ActionResult GetFlowById(long id)
    {
        var flow = _manager.GetFlowByIdWithTheme(id);

        if (flow == null)
            return NotFound();

        return Ok(new FlowViewModel
        {
            Id = flow.Id,
            FlowType = flow.FlowType,
            Steps = flow.Steps,
            Participations = flow.Participations,
            ThemeId = flow.Theme.Id
        });
    }
}