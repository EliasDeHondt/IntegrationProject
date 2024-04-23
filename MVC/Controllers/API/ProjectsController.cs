using Business_Layer;
using Domain.Platform;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("SetProject/{id}")]
    public IActionResult AddProject(long id)
    {
        _uow.BeginTransaction();
        
        SharedPlatform sharedPlatform = _sharedPlatformManager.GetSharedPlatform(1);
        _projectManager.AddProject(sharedPlatform, id);
        
        _uow.Commit();
        return CreatedAtAction("AddProject", new Project());
    }


}