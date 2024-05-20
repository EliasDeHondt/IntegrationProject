/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : Controller
{
    private readonly FlowManager _manager;
    private readonly QuestionManager _Qmanager;
    private readonly UnitOfWork _uow;

    public StatisticsController(FlowManager manager,QuestionManager qmanager, UnitOfWork uow)
    {
        _manager = manager;
        _Qmanager = qmanager;
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
    
    // [HttpGet("GetAnswersPerQuestion/{id}")]
    // public IActionResult GetAnswersPerQuestion(long flowid)
    // {
    //     var flowQuestions = _manager.GetAnswersPerQuestion(flowid);
    //
    //     return Ok(flowQuestions);
    // }
    [HttpGet("GetQuestionNames/{flowname}")]
    public IActionResult GetQuestionNames(string flowname)
    {
        var flowCountQuestions = _manager.GetQuestionNames(flowname);
        

        return Ok(flowCountQuestions);
    }
    [HttpGet("GetChoicesNames/{question}")]
    public IActionResult GetChoicesNames(string question)
    {
        var flowCountQuestions = _Qmanager.GetChoicesNames("If you were to prepare the budget for your city or municipality, where would you mainly focus on in the coming years? Choose one.");
        

        return Ok(flowCountQuestions);
    }
    
    [HttpGet("GetAnswerCountsForQuestions/{question}")]
    public IActionResult GetAnswerCountsForQuestions(string question)
    {
        var answerCountQuestions = _Qmanager.GetAnswerCountsForQuestions("If you were to prepare the budget for your city or municipality, where would you mainly focus on in the coming years? Choose one.");
        
    
        return Ok(answerCountQuestions);
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
}