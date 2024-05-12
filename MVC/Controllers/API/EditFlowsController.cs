using Business_Layer;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class EditFlowsController : Controller
{
    private readonly FlowManager _manager;
    private readonly StepManager _stepManager;
    private readonly UnitOfWork _uow;

    public EditFlowsController(FlowManager manager, StepManager stepManager, UnitOfWork uow)
    {
        _manager = manager;
        _stepManager = stepManager;
        _uow = uow;
    }

    [HttpGet("/EditFlows/GetSteps/{flowId:long}")]
    public IActionResult GetSteps(long flowId)
    {
        var steps = _stepManager.GetAllStepsForFlow(flowId);
        return Ok(steps.Select(step => StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>(step)));
    }

    [HttpPost("/EditFlows/CreateStep/{flowId:long}/{stepNumber:int}/{stepType}")]
    public IActionResult CreateStep(long flowId, int stepNumber, string stepType)
    {
        _uow.BeginTransaction();
        
        StepBase step = _manager.CreateStep(flowId, stepNumber, stepType);

        _uow.Commit();

        return Created("CreateStep", step);
    }

    [HttpPost("/EditFlows/CreateChoice/{flowId:long}/{stepNr:int}")]
    public IActionResult CreateChoice(long flowId, int stepNr)
    {
        _uow.BeginTransaction();

        Choice choice = _stepManager.CreateChoice(flowId, stepNr);
        
        _uow.Commit();

        return Created("CreateChoice", choice);
    }

    [HttpPost("/EditFlows/CreateInformation/{flowId:long}/{stepNr:int}/{type}")]
    public IActionResult CreateInformation(long flowId, int stepNr, string type)
    {
        _uow.BeginTransaction();

        InformationBase information = _stepManager.CreateInformation(flowId, stepNr, type);
        
        _uow.Commit();

        return Created("CreateChoice", information);
    }

    [HttpGet("/EditFlows/GetStepId/{flowId:long}/{stepNr:int}")]
    public IActionResult GetStepId(long flowId, int stepNr)
    {
        var stepId = _stepManager.GetStepId(flowId, stepNr);
        return Ok(stepId);
    }

}