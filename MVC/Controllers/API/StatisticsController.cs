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
    private readonly UnitOfWork _uow;

    public StatisticsController(FlowManager manager, UnitOfWork uow)
    {
        _manager = manager;
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
        var flowCountQuestions = _manager.GetQuestionsFromFlow(flowname);
        var a = flowCountQuestions.Count();

        return Ok(flowCountQuestions);
    }
    
    // [HttpGet("GetQuestionsFromFlow/{id}")]
    // public IActionResult GetQuestionsFromFlow(long flowid)
    // {
    //     var flowQuestions = _manager.GetQuestionsFromFlow(flowid);
    //
    //     return Ok(flowQuestions);
    // }
}