using Business_Layer;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(policy: "admin")]
    public IActionResult GetSteps(long flowId)
    {
        try
        {
            var steps = _stepManager.GetAllStepsForFlow(flowId);
            return Ok(steps.Select(step => StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>(step)));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPost("/EditFlows/CreateStep/{flowId:long}/{stepNumber:int}/{stepType}")]
    [Authorize(policy: "admin")]
    public IActionResult CreateStep(long flowId, int stepNumber, string stepType)
    {
        try
        {
            _uow.BeginTransaction();
        
            StepBase step = _manager.CreateStep(flowId, stepNumber, stepType);

            _uow.Commit();

            return Created("CreateStep", step);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPost("/EditFlows/CreateChoice/{flowId:long}/{stepNr:int}")]
    [Authorize(policy: "admin")]
    public IActionResult CreateChoice(long flowId, int stepNr)
    {
        try
        {
            _uow.BeginTransaction();

            Choice choice = _stepManager.CreateChoice(flowId, stepNr);
        
            _uow.Commit();

            return Created("CreateChoice", choice);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPost("/EditFlows/CreateInformation/{flowId:long}/{stepNr:int}/{type}")]
    [Authorize(policy: "admin")]
    public IActionResult CreateInformation(long flowId, int stepNr, string type)
    {
        try
        {
            _uow.BeginTransaction();

            InformationBase information = _stepManager.CreateInformation(flowId, stepNr, type);
        
            _uow.Commit();

            return Created("CreateChoice", information);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("/EditFlows/GetStepId/{flowId:long}/{stepNr:int}")]
    [Authorize(policy: "admin")]
    public IActionResult GetStepId(long flowId, int stepNr)
    {
        try
        {
            var stepId = _stepManager.GetStepId(flowId, stepNr);
            return Ok(stepId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

}