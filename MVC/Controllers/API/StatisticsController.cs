using Business_Layer;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : Controller
{
    private readonly FlowManager _manager;
    private readonly QuestionManager _Qmanager;
    private readonly ProjectManager _projectManager;
    private readonly UnitOfWork _uow;

    public StatisticsController(FlowManager manager,QuestionManager qmanager, ProjectManager projectManager, UnitOfWork uow)
    {
        _manager = manager;
        _Qmanager = qmanager;
        _projectManager = projectManager;
        _uow = uow;
    }
    
    [HttpGet("GetRespondentCountFromProject/{projectId:long}")]
    public IActionResult GetRespondentCountFromProject(long projectId)
    {
        var count = _projectManager.GetRespondentCountFromProject(projectId);
        
        return Ok(count);
    }
    
    [HttpGet("GetFlowCountFromProject/{projectId:long}")]
    public IActionResult GetFlowCountFromProject(long projectId)
    {
        var count = _projectManager.GetFlowCountFromProject(projectId);
        
        return Ok(count);
    }
    
    [HttpGet("GetSubThemeCountFromProject/{projectId:long}")]
    public IActionResult GetSubThemeCountFromProject(long projectId)
    {
        var count = _projectManager.GetSubThemeCountFromProject(projectId);
        
        return Ok(count);
    }

}
