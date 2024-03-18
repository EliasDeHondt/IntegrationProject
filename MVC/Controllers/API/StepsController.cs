/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

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
        // if (stepBase.Flow.FlowType.GetType().Name == FlowType.CIRCULAR.GetType().Name)
        // {
        //     
        // }
       // while (stepBase == null && _manager.GetFlowType(flowId) == FlowType.CIRCULAR)
       // if (stepBase.Flow.FlowType.GetType().Name == FlowType.CIRCULAR.GetType().Name)
        // if (stepNumber == 3)
        // {
        //     stepNumber = 1;
        //     stepBase = _manager.GetStepForFlowByNumber(flowId, stepNumber);
        // }
        switch (stepBase)
        {
            case CombinedStep cStep:
            {
                CombinedStepViewModel stepViewModel = CreateCominedStepViewModel(cStep);
                return Ok(stepViewModel);
            }
            case InformationStep iStep:
            {
                InformationStepViewModel stepViewModel = CreateInformationStepViewModel(iStep);

                return Ok(stepViewModel);
            }
            case null:
                // if (stepBase.Flow.FlowType.GetType().Name == FlowType.CIRCULAR.GetType().Name)
                // {
                //     stepNumber = 1;
                // }
                return NoContent();
            default:
                return Ok(stepBase);
        }
    }

    private InformationViewModel CreateInformationViewModel(InformationBase information)
    {
        return new InformationViewModel
        {
            Id = information.Id,
            Information = information.GetInformation(),
            InformationType = information.GetType().Name
        };
    }

    private CombinedStepViewModel CreateCominedStepViewModel(CombinedStep step)
    {
        return new CombinedStepViewModel
        {
            Id = step.Id,
            InformationViewModel = CreateInformationViewModel(step.InformationBase),
            StepNumber = step.StepNumber
        };
    }
    
    private InformationStepViewModel CreateInformationStepViewModel(InformationStep step)
    {
        return new InformationStepViewModel
        {
            Id = step.Id,
            InformationViewModel = CreateInformationViewModel(step.InformationBase),
            StepNumber = step.StepNumber
        };
    }
    
}