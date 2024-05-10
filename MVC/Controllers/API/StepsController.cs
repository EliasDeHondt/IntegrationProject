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

    public StepsController(StepManager manager)
    {
        _manager = manager;
    }

    [HttpGet("GetNextStep/{flowId:int}/{stepNumber:long}")]
    public ActionResult GetNextStep(int stepNumber, long flowId)
    {
        StepBase stepBase = _manager.GetStepForFlowByNumber(flowId, stepNumber);

        var stepViewModel = StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>(stepBase);

        return Ok(stepViewModel);
    }

    [HttpGet("GetConditionalNextStep/{flowId:long}/{contentId:long}")]
    public ActionResult GetConditionalNextStep(long flowId, long contentId)
    {
        StepBase stepBase = _manager.GetStepForFlowByContentId(flowId, contentId);

        var stepViewModel = StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>(stepBase);

        return Ok(stepViewModel);
    }

    [HttpPost("/{flowId:long}/Update/{stepNr:int}")]
    public IActionResult UpdateStep(long flowId, int stepNr, StepViewModel step)
    {
        var newStep = _manager.GetStepForFlowByNumber(flowId, stepNr);

        if (newStep == null)
            return NotFound();

        _manager.ChangeStep(newStep);

        return NoContent();
    }
}