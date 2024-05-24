using Business_Layer;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.projectModels;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : Controller
{

    private readonly ProjectManager _projectManager;
    private readonly SharedPlatformManager _sharedPlatformManager;
    private readonly FlowManager _flowmanager;
    private readonly UnitOfWork _uow;

    public ProjectsController(ProjectManager projectManager, SharedPlatformManager sharedPlatformManager, UnitOfWork uow)
    {
        _projectManager = projectManager;
        _sharedPlatformManager = sharedPlatformManager;
        _uow = uow;
    }

    [HttpGet("GetProjectsForSharedPlatform/{id}")]
    public IEnumerable<Project> GetProjectsForSharedPlatform(long id)
    {
        return _projectManager.GetAllProjectsForSharedPlatformWithMainTheme(id);
    }

    [HttpPost("AddProject")]
    public IActionResult AddProject(ProjectDto model)
    {
        _uow.BeginTransaction();
        
        var project = new Project
        {
            Title = model.Title,
            Description = model.Description,
            SharedPlatform = _sharedPlatformManager.GetSharedPlatform(model.SharedPlatformId)
        };
        
        // If the logo is also added (Image is not null)
        if (model.Image != null) project.Image = model.Image;

        _projectManager.CreateProject(project);
        _sharedPlatformManager.AddProjectToPlatform(project, model.SharedPlatformId);
        
        _uow.Commit();

        return Created("CreateProject",  project);
    }
    
    [HttpPut("UpdateProject/{projectId}")]
    public IActionResult UpdateProject(long projectId, ProjectDto model)
    {
        _uow.BeginTransaction();
        _projectManager.ChangeProject(projectId, model.Title, model.Description);
        _uow.Commit();
        return NoContent();
    }
    
    [HttpGet("GetProjectsForPlatform/{platformId}")]
    public IActionResult GetProjectsForPlatform(long platformId)
    {
        if (User.Identity is { IsAuthenticated: false }) return Unauthorized();
        var projects = _sharedPlatformManager.GetProjectsForPlatform(platformId);

        ICollection<ProjectViewModel> projectList = new List<ProjectViewModel>();
        foreach (var project in projects)
        {
            projectList.Add(new ProjectViewModel
            {
                Id = project.Id,
                Name = project.Title,
                Description = project.Description
            });
        }

        return Ok(projectList);
    }
    
    [HttpGet("GetProjectWithId/{projectId}")]
    public IActionResult GetProjectWithId(int projectId)
    {
        Project project = _projectManager.GetProjectWithSharedPlatformAndMainTheme(projectId);

        if (project == null)
        {
            return NotFound();
        }
        
        ProjectDto projectDto = new ProjectDto
        {
            MainThemeId = project.MainTheme.Id,
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            SharedPlatformId = project.SharedPlatform.Id
        };

        return Ok(projectDto); 
    }
    
    [HttpGet("GetFlowsForProject/{projectId}")]
    public IActionResult GetFlowsForProject(int projectId)
    {
        var flows = _projectManager.GetFlowsForProjectById(projectId);

        return Ok(flows);

    }
    
    [HttpPost("CreateProjectFlow/{flowType}/{themeId}")]
    public IActionResult CreateFlow(string flowType,int themeId)
    {

        FlowType type = Enum.Parse<FlowType>(flowType);
        
        _uow.BeginTransaction();
        
        Flow flow = _projectManager.CreateFlowForProject(type, themeId);
        
        _uow.Commit();
        
        return Created("CreateFlow", flow);
    }

    [HttpGet("GetNotesForProject/{projectId:long}")]
    public IActionResult GetNotesForProject(long projectId)
    {
        var flows = _projectManager.GetNotesForProjectById(projectId);

        return Ok(flows.Select(flow => new FlowViewModel
        {
            Id = flow.Id,
            FlowType = flow.FlowType,
            Steps = flow.Steps,
            Participations = flow.Participations,
            ThemeId = flow.Theme.Id
        }));
    }
    
    // [HttpPost("GetNotesForProject/{projectId:long}")]
    // public IActionResult ToggleProjectClosed()
    // {
    //
    //     yourClassInstance.ProjectClosed = !yourClassInstance.ProjectClosed;
    //     return Ok(new { success = true, projectClosed = yourClassInstance.ProjectClosed });
    // }
}