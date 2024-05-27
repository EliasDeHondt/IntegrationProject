/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.Text.Json;
using System.Text.Json.Serialization;
using Business_Layer;
using Domain.Accounts;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("GetNextStep/{flowId}/{stepNumber}")]
    [Authorize(policy: "flowAccess")]
    public ActionResult GetNextStep(int stepNumber, long flowId)
    {
        try
        {
            StepBase stepBase = _manager.GetStepForFlowByNumber(flowId, stepNumber);

            var stepViewModel = StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>(stepBase);

            return Ok(stepViewModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetConditionalNextStep/{stepId}")]
    [Authorize(policy: "flowAccess")]
    public ActionResult GetConditionalNextStep(long stepId)
    {
        try
        {
            StepBase stepBase = _manager.GetStepById(stepId);

            var stepViewModel = StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>(stepBase);

            return Ok(stepViewModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPost("AddNote/{flowId}/{stepNr}/{note}")]
    [Authorize(Roles = UserRoles.Facilitator)]
    public ActionResult AddNote(long flowId, int stepNr, string note)
    {
        try
        {
            _uow.BeginTransaction();

            var newNote = _manager.AddNote(flowId, stepNr, note);

            _uow.Commit();

            return Created("AddNote", newNote);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetStepsFromFlow/{flowId}")]
    [Authorize(policy: "flowAccess")]
    public ActionResult GetStepsFromFlow(long flowId)
    {
        try
        {
            var steps = _manager.GetAllStepsForFlow(flowId);

            var stepViewModels = steps.Select(StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>).ToList();

            return Ok(stepViewModels);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPut("UpdateInformationStep")]
    [Authorize(policy: "projectAccess")]
    public ActionResult UpdateInformationStep(InformationStepViewModel model)
    {
        try
        {
            _uow.BeginTransaction();

            var step = _manager.GetStepById(model.Id);

            if (step is InformationStep infoStep)
            {
                step.StepNumber = model.StepNumber;
            
                infoStep.InformationBases = model.InformationViewModel
                    .Select(infoViewModel =>
                    {
                        var info = _manager.GetInformationById(infoViewModel.Id);
                        _manager.ChangeInformation(info, infoViewModel.Information);
                        return info;
                    }).ToList();
            }

            _uow.Commit();

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPut("UpdateQuestionStep")]
    [Authorize(policy: "projectAccess")]
    public ActionResult UpdateQuestionStep(QuestionStepViewModel model)
    {
        try
        {
            _uow.BeginTransaction();

            var step = _manager.GetStepById(model.Id);

            if (step is QuestionStep questionStep)
            {
                step.StepNumber = model.StepNumber;
            
                var question = questionStep.QuestionBase;
                question.Question = model.QuestionViewModel.Question;
                if (question is ChoiceQuestionBase choiceQuestion)
                {
                    if (model.QuestionViewModel.Choices != null)
                        choiceQuestion.Choices = model.QuestionViewModel.Choices.Select<ChoiceViewModel, Choice>(
                            choiceViewModel =>
                            {
                                var choice = _manager.GetChoiceById(choiceViewModel.Id);
                                _manager.ChangeChoice(choice, choiceViewModel.Text, choiceViewModel.NextStepId);
                                return choice;
                            }).ToList();
                }
            }

            _uow.Commit();

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpPut("UpdateQuestionStepByNumber/{stepId}/{stepNumber}")]
    [Authorize(policy: "projectAccess")]
    public ActionResult UpdateQuestionStepByNumber(long stepId, int stepNumber)
    {
        try
        {
            _uow.BeginTransaction();
    
            var step = _manager.GetStepById(stepId);

            step.StepNumber = stepNumber;
    
            _uow.Commit();
    
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    [HttpPut("UpdateInfoStepByNumber/{stepId}/{stepNumber}")]
    [Authorize(policy: "projectAccess")]
    public ActionResult UpdateInfoStepByNumber(long stepId, int stepNumber)
    {
        try
        {
            _uow.BeginTransaction();
    
            var step = _manager.GetStepById(stepId);

            step.StepNumber = stepNumber;
    
            _uow.Commit();
    
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}