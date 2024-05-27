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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : Controller
{
    private readonly FlowManager _manager;
    private readonly QuestionManager _qmanager;
    private readonly SharedPlatformManager _platformManager;
    private readonly ProjectManager _projectManager;

    public StatisticsController(FlowManager manager,QuestionManager qmanager, SharedPlatformManager platformManager, ProjectManager projectManager)
    {
        _manager = manager;
        _qmanager = qmanager;
        _platformManager = platformManager;
        _projectManager = projectManager;
    }

    [HttpGet("GetNamesPerFlow")]
    [Authorize(policy: "admin")]
    public IActionResult GetNamesPerFlow()
    {
        try
        {
            var flowNames = _manager.GetNamesPerFlow();

            return Ok(flowNames);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetCountStepsPerFlow")]
    [Authorize(policy: "admin")]
    public IActionResult GetCountStepsPerFlow()
    {
        try
        {
            var flowCounts = _manager.GetCountStepsPerFlow();

            return Ok(flowCounts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetCountParticipationsPerFlow")]
    [Authorize(policy: "admin")]
    public IActionResult GetCountParticipationsPerFlow()
    {
        try
        {
            var flowCounts = _manager.GetCountParticipationsPerFlow();

            return Ok(flowCounts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetQuestionsFromFlow/{flowname}")]
    [Authorize(policy: "admin")]
    public IActionResult GetQuestionsFromFlow(string flowname)
    {
        try
        {
            var flowCountQuestions = _manager.GetQuestionCountsForFlow(flowname);
            return Ok(flowCountQuestions);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetRespondentCountFromPlatform/{platformId:long}")]
    [Authorize(policy: "admin")]
    public IActionResult GetRespondentCountFromPlatform(long platformId)
    {
        try
        {
            var count = _platformManager.GetRespondentCountFromPlatform(platformId);
        
            return Ok(count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetPlatformOrganisation/{platformId:long}")]
    [Authorize(policy: "admin")]
    public IActionResult GetPlatformOrganisation(long platformId)
    {
        try
        {
            var count = _platformManager.GetPlatformOrganisation(platformId);
        
            return Ok(count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetRespondentCountFromProject/{projectId:long}")]
    [Authorize(policy: "admin")]
    public IActionResult GetRespondentCountFromProject(long projectId)
    {
        try
        {
            var count = _projectManager.GetRespondentCountFromProject(projectId);
            return Ok(count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }
    }

    [HttpGet("GetQuestionNames/{flowname}")]
    [Authorize(policy: "admin")]
    public IActionResult GetQuestionNames(string flowname)
    {
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
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }

    [HttpGet("GetFlowCountFromProject/{projectId:long}")]
    [Authorize(policy: "admin")]
    public IActionResult GetFlowCountFromProject(long projectId)
    {
        try
        {
            var count = _projectManager.GetFlowCountFromProject(projectId);

            return Ok(count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetChoicesNames/{question}")]
    [Authorize(policy: "admin")]
    public IActionResult GetChoicesNames(long question)
    {
        try
        {
            var choicesNames = _qmanager.GetChoicesNames(question);

            return Ok(choicesNames);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetAnswerCountsForQuestions/{question}")]
    [Authorize(policy: "admin")]
    public IActionResult GetAnswerCountsForQuestions(long question)
    {
        try
        {
            var answerCountQuestions = _qmanager.GetAnswerCountsForQuestions(question);
            return Ok(answerCountQuestions);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetSubThemeCountFromProject/{projectId:long}")]
    [Authorize(policy: "admin")]
    public IActionResult GetSubThemeCountFromProject(long projectId)
    {
        try
        {
            var count = _projectManager.GetSubThemeCountFromProject(projectId);

            return Ok(count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetRespondentsFromFlow/{flowname}")]
    [Authorize(policy: "admin")]
    public IActionResult GetRespondentsFromFlow(string flowname)
    {
        try
        {
            var respondentCountQuestions = _manager.GetRespondentCountsFromFlow(flowname);

            return Ok(respondentCountQuestions);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetParticipatoinNames/{flowname}")]
    [Authorize(policy: "admin")]
    public IActionResult GetParticipationNames(string flowname)
    {
        try
        {
            var participations = _manager.GetParticipationNames(flowname);
        
            return Ok(participations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetAnswersFromQuestion/{questionId:long}")]
    [Authorize(policy: "admin")]
    public IActionResult GetAnswersFromQuestion(long questionId)
    {
        try
        {
            var answers = _qmanager.GetAnswersFromQuestion(questionId);

            return Ok(answers);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetQuestionText/{questionId:long}")]
    [Authorize(policy: "admin")]
    public IActionResult GetQuestionText(long questionId)
    {
        try
        {
            var question = _qmanager.GetQuestionText(questionId);

            return Ok(question);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetQuestionType/{questionId:long}")]
    [Authorize(policy: "admin")]
    public IActionResult GetQuestionType(long questionId)
    {
        try
        {
            var question = _qmanager.GetQuestionType(questionId);

            return Ok(question);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}