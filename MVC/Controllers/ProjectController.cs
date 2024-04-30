using Business_Layer;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class ProjectController: Controller
{
    private readonly ProjectManager _manager;
    
    public ProjectController(ProjectManager manager)
    {
        _manager = manager;
    }

    public IActionResult ProjectPage(long id, bool isMainThemeId = false)
    {
        var project = isMainThemeId ? _manager.GetProjectThroughMainTheme(id) : _manager.GetProject(id);
        return View(project);
    }
}