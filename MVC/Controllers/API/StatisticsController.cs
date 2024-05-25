/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps.Questions;
using Domain.ProjectLogics.Steps.Questions.Answers;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : Controller
{
    private readonly FlowManager _manager;
    private readonly QuestionManager _Qmanager;
    private readonly ProjectManager _projectManager;
    private readonly UnitOfWork _uow;

    public StatisticsController(FlowManager manager, QuestionManager qmanager, ProjectManager projectManager,
        UnitOfWork uow)
    {
        _manager = manager;
        _Qmanager = qmanager;
        _projectManager = projectManager;
        _uow = uow;
    }

    [HttpGet("GetNamesPerFlow")]
    public IActionResult GetNamesPerFlow()
    {
        var flowNames = _manager.GetNamesPerFlow();

        return Ok(flowNames);
    }

    [HttpGet("GetCountStepsPerFlow")]
    public IActionResult GetCountStepsPerFlow()
    {
        var flowCounts = _manager.GetCountStepsPerFlow();

        return Ok(flowCounts);
    }

    [HttpGet("GetCountParticipationsPerFlow")]
    public IActionResult GetCountParticipationsPerFlow()
    {
        var flowCounts = _manager.GetCountParticipationsPerFlow();

        return Ok(flowCounts);
    }

    [HttpGet("GetQuestionsFromFlow/{flowname}")]
    public IActionResult GetQuestionsFromFlow(string flowname)
    {
        var flowCountQuestions = _manager.GetQuestionCountsForFlow(flowname);
        return Ok(flowCountQuestions);
    }

    [HttpGet("GetRespondentCountFromProject/{projectId:long}")]
    public IActionResult GetRespondentCountFromProject(long projectId)
    {
        var count = _projectManager.GetRespondentCountFromProject(projectId);
        return Ok(count);
    }

    [HttpGet("GetQuestionNames/{flowname}")]
    public IActionResult GetQuestionNames(string flowname)
    {
        // var flowCountQuestions = _manager.GetQuestionNames(flowname);
        // return Ok(flowCountQuestions);
        try
        {
            var questions = _manager.GetQuestionNames(flowname);

            var result = questions.Select(q => new QuestionViewModel
            {
                Id = q.Id,
                Question = q.Question
            }).ToList();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("GetFlowCountFromProject/{projectId:long}")]
    public IActionResult GetFlowCountFromProject(long projectId)
    {
        var count = _projectManager.GetFlowCountFromProject(projectId);

        return Ok(count);
    }

    [HttpGet("GetChoicesNames/{question}")]
    public IActionResult GetChoicesNames(long question)
    {
        var choicesNames = _Qmanager.GetChoicesNames(question);

        return Ok(choicesNames);
    }

    [HttpGet("GetAnswerCountsForQuestions/{question}")]
    public IActionResult GetAnswerCountsForQuestions(long question)
    {
        var answerCountQuestions = _Qmanager.GetAnswerCountsForQuestions(question);


        return Ok(answerCountQuestions);
    }

    [HttpGet("GetSubThemeCountFromProject/{projectId:long}")]
    public IActionResult GetSubThemeCountFromProject(long projectId)
    {
        var count = _projectManager.GetSubThemeCountFromProject(projectId);

        return Ok(count);
    }

    [HttpGet("GetRespondentsFromFlow/{flowname}")]
    public IActionResult GetRespondentsFromFlow(string flowname)
    {
        var respondentCountQuestions = _manager.GetRespondentCountsFromFlow(flowname);


        return Ok(respondentCountQuestions);
    }

    [HttpGet("GetParticipatoinNames/{flowname}")]
    public IActionResult GetParticipationNames(string flowname)
    {
        var participations = _manager.GetParticipationNames(flowname);


        return Ok(participations);
    }

    [HttpGet("GetAnswersFromQuestion/{questionId:long}")]
    public IActionResult GetAnswersFromQuestion(long questionId)
    {
        var answers = _Qmanager.GetAnswersFromQuestion(questionId);

        return Ok(answers);
    }

    [HttpGet("GetQuestionText/{questionId:long}")]
    public IActionResult GetQuestionText(long questionId)
    {
        var question = _Qmanager.GetQuestionText(questionId);

        return Ok(question);
    }

    [HttpGet("GetQuestionType/{questionId:long}")]
    public IActionResult GetQuestionType(long questionId)
    {
        var question = _Qmanager.GetQuestionType(questionId);

        return Ok(question);
    }
}