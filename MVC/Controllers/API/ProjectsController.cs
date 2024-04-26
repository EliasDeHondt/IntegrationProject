using Business_Layer;
using Domain.Platform;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

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

    [HttpPost("AddProject/{mainTheme}/{sharedPlatformid}")]
    public IActionResult AddProject(string mainTheme,long sharedPlatformid)
    {
        _uow.BeginTransaction();

        MainTheme theme = new MainTheme(mainTheme);
        sharedPlatformid = 2; //todo
        long id = _projectManager.ProjectCount().ToList().Count()+1;
        SharedPlatform sharedPlatform = _sharedPlatformManager.GetSharedPlatform(sharedPlatformid);
        _projectManager.UpdateProject(theme,sharedPlatform, id);
        
        _uow.Commit();
        return CreatedAtAction("AddProject", new Project(theme,sharedPlatform, id));
    }
    
    [HttpPost("CreateProject")]
    public IActionResult CreateProject(ProjectViewModel model)
    {
        long sharedPlatformid = 2; //todo
        
        _uow.BeginTransaction();

        var project = new Project
        {
            Title = model.Name,
            Description = model.Description,
            SharedPlatform = _sharedPlatformManager.GetSharedPlatform(model.SharedPlatformId) 
        };
        _projectManager.CreateProject(project);
        
        _uow.Commit();

        return Created("CreateProject",  project);
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
                Name = project.Title,
                Description = project.Description
            });
        }
        // var a = new ProjectViewModel
        // {
        //     Name = "ASfsadfd",
        //     Description = "ufvgyhjksd"
        // };
        // projectList.Add(a);
        return Ok(projectList);
    }
}