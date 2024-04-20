/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

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
        
        switch (stepBase)
        {
            case CombinedStep cStep:
            {
                CombinedStepViewModel stepViewModel = StepModelFactory.CreateCombinedStepViewModel(cStep);
                return Ok(stepViewModel);
            }
            case InformationStep iStep:
            {
                InformationStepViewModel stepViewModel = StepModelFactory.CreateInformationStepViewModel(iStep);

                return Ok(stepViewModel);
            }
            case QuestionStep qStep:
            {
                QuestionStepViewModel stepViewModel = StepModelFactory.CreateQuestionStepViewModel(qStep);

                return Ok(stepViewModel);
            }
            case null:
                return BadRequest();
            default:
                return Ok(stepBase);
        }
    }
    
    
    
    
}