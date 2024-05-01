using Business_Layer;
using Domain.ProjectLogics.Steps;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class EditFlowsController : Controller
{
    private readonly FlowManager _manager;
    private readonly UnitOfWork _uow;

    public EditFlowsController(FlowManager manager, UnitOfWork uow)
    {
        _manager = manager;
        _uow = uow;
    }

    [HttpGet("/EditFlows/GetSteps/{flowId:long}")]
    public IActionResult GetSteps(long flowId)
    {
        var steps = _manager.GetAllStepsInFlow(flowId);

        return Ok(steps);
    }

    [HttpPost("/EditFlows/CreateStep/{flowId:long}/{stepNumber:int}/{stepType}")]
    public IActionResult CreateStep(long flowId, int stepNumber, string stepType)
    {
        _uow.BeginTransaction();
        
        StepBase step = _manager.AddStep(flowId, stepNumber, stepType);

        _uow.Commit();

        return Created("CreateStep", step);
    }

}