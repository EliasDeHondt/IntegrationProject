using System.Collections;
using Business_Layer;
using Domain.Accounts;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class FlowsController : Controller
{
    private readonly FlowManager _manager;
    private readonly UnitOfWork _uow;

    public FlowsController(FlowManager manager, UnitOfWork uow)
    {
        _manager = manager;
        _uow = uow;
    }

    [HttpPost("SetRespondentEmail/{flowId:int}/{inputEmail}")]
    public IActionResult SetRespondentEmail(int flowId, string inputEmail)
    {
        try
        {
            _uow.BeginTransaction();
            _manager.SetParticipationByFlow(flowId, inputEmail);
            _uow.Commit();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPut("{id}/{state}")]
    [Authorize(Roles = UserRoles.Facilitator)]
    public IActionResult PutFlowState(long id, string state)
    {
        try
        {
            Flow flow = _manager.GetFlowByIdWithTheme(id);

            if (flow == null)
                return NotFound();

            if (Enum.TryParse(state, out FlowState flowState))
                flow.State = flowState;
            _manager.ChangeFlowState(flow);

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("{id}")]
    [Authorize(policy: "flowAccess")]
    public IActionResult GetFlow(long id)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Authorize(policy: "flowAccess")]
    public ActionResult GetFlows()
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("{type}")]
    [Authorize(policy: "flowAccess")]
    public ActionResult GetFlowsByType(string type)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("{id:long}")]
    [Authorize(policy: "flowAccess")]
    public ActionResult GetFlowById(long id)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetFlowsForProject/{id}")]
    [Authorize(policy: "flowAccess")]
    public IActionResult GetFlowsForProject(long id)
    {
        try
        {
            var flows = _manager.GetFlowsByProject(id);
            return Ok(flows.Select(flow => new FlowViewModel
            {
                Id = flow.Id,
                FlowType = flow.FlowType,
                Steps = flow.Steps,
                Participations = flow.Participations,
                ThemeId = flow.Theme.Id
            }));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}