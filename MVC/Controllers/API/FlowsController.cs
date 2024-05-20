using System.Collections;
using Business_Layer;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
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
    private readonly StepManager _stepManager;
    private readonly UnitOfWork _uow;

    public FlowsController(FlowManager manager, StepManager stepManager, UnitOfWork uow)
    {
        _manager = manager;
        _stepManager = stepManager;
        _uow = uow;
    }

    [HttpPost("SetRespondentEmail/{flowId:int}/{inputEmail}")]
    public IActionResult SetRespondentEmail(int flowId, string inputEmail)
    {
        _manager.SetParticipationByFlow(flowId, inputEmail);

        return Ok();
    }

    [HttpPut("{id}/{state}")]
    public IActionResult PutFlowState(long id, string state)
    {
        Flow flow = _manager.GetFlowByIdWithTheme(id);

        if (flow == null)
            return NotFound();

        if (Enum.TryParse(state, out FlowState flowState))
            flow.State = flowState;
        _manager.ChangeFlowState(flow);

        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult GetFlow(long id)
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

    [HttpPut("/{flowId}/Update")]
    public IActionResult UpdateFlow(long flowId, IEnumerable<StepViewModel> model)
    {
        _uow.BeginTransaction();

        var flow = _manager.GetFlowWithSteps(flowId);

        var steps = model.Select<StepViewModel, StepBase>(stepViewModel =>
        {
            var step = flow.Steps.FirstOrDefault(s => s.Id == stepViewModel.Id);

            switch (step)
            {
                case InformationStep infoStep when stepViewModel is InformationStepViewModel infoStepViewModel:
                    infoStep.InformationBases = infoStepViewModel.InformationViewModel
                        .Select(infoViewModel =>
                        {
                            var info = _stepManager.GetInformationById(infoViewModel.Id);
                            info = _stepManager.ChangeInformation(info, infoViewModel.Information);
                            return info;
                        }).ToList();
                    return infoStep;
                    
                case QuestionStep questionStep when stepViewModel is QuestionStepViewModel questionStepViewModel:
                    var question = _stepManager.GetQuestionById(questionStepViewModel.QuestionViewModel.Id);
                    question.Question = questionStepViewModel.QuestionViewModel.Question;
                    if (question is ChoiceQuestionBase choiceQuestion)
                    {
                        choiceQuestion.Choices = questionStepViewModel.QuestionViewModel.Choices.Select(choiceViewModel =>
                        {
                            var choice = _stepManager.GetChoiceById(choiceViewModel.Id);
                            choice.Text = choiceViewModel.Text;
                            choice.NextStep = _stepManager.GetStepById(choiceViewModel.NextStepId);
                            return choice;
                        }).ToList();
                    }
                    questionStep.QuestionBase = question;
                    return questionStep;
                    
                default:
                    return step;
            }
        }).ToList();

        flow.Steps = steps;
        
        _uow.Commit();

        return NoContent();
    }

    [HttpGet]
    public ActionResult GetFlows()
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

    [HttpGet("{type}")]
    public ActionResult GetFlowsByType(string type)
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

    [HttpGet("{id:long}")]
    public ActionResult GetFlowById(long id)
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

    [HttpGet("GetFlowsForProject/{id}")]
    public IActionResult GetFlowsForProject(long id)
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
}