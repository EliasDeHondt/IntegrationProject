using System.Drawing;
using Business_Layer;
using Domain.Platform;
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
    public IActionResult AddProject(ProjectViewModel model)
    {
        _uow.BeginTransaction();
        
        var project = new Project
        {
            Title = model.Name,
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
}