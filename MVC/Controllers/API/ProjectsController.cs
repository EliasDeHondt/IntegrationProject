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

    [HttpPost("AddProject/{id}/{mainTheme}/{sharedPlatformid}")]
    public IActionResult AddProject(long id, string theme,long sharedPlatformid)
    {
        _uow.BeginTransaction();

        MainTheme mainTheme = new MainTheme(theme);
        sharedPlatformid = 2;
        SharedPlatform sharedPlatform = _sharedPlatformManager.GetSharedPlatform(sharedPlatformid);
        _projectManager.AddProject(mainTheme,sharedPlatform, id);
        
        _uow.Commit();
        return CreatedAtAction("AddProject", new Project(mainTheme,sharedPlatform, id));
    }


}