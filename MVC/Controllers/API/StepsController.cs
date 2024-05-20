/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.Text.Json;
using System.Text.Json.Serialization;
using Business_Layer;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class StepsController : Controller
{
    private readonly StepManager _manager;
    private readonly UnitOfWork _uow;

    public StepsController(StepManager manager, UnitOfWork uow)
    {
        _manager = manager;
        _uow = uow;
    }

    [HttpGet("GetNextStep/{flowId:int}/{stepNumber:long}")]
    public ActionResult GetNextStep(int stepNumber, long flowId)
    {
        StepBase stepBase = _manager.GetStepForFlowByNumber(flowId, stepNumber);

        var stepViewModel = StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>(stepBase);

        return Ok(stepViewModel);
    }

    [HttpGet("GetConditionalNextStep/{stepId:long}")]
    public ActionResult GetConditionalNextStep(long stepId)
    {
        StepBase stepBase = _manager.GetStepById(stepId);

        var stepViewModel = StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>(stepBase);

        return Ok(stepViewModel);
    }

    [HttpPost("AddNote/{flowId}/{stepNr}/{note}")]
    public ActionResult AddNote(long flowId, int stepNr, string note)
    {
        _uow.BeginTransaction();

        var newNote = _manager.AddNote(flowId, stepNr, note);
        
        _uow.Commit();

        return Created("AddNote", newNote);
    }
}