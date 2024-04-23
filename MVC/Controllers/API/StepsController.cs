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
    
    
    
    
}