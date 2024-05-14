using Business_Layer;
using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class ProjectController: Controller
{
    private readonly ProjectManager _manager;
    
    public ProjectController(ProjectManager manager)
    {
        _manager = manager;
    }
    
    [Authorize(Roles = UserRoles.ProjectPermission)]
    public IActionResult ProjectPage(long id, bool isMainThemeId = false)
    {
        var project = isMainThemeId ? _manager.GetProjectThroughMainTheme(id) : _manager.GetProject(id);
        return View(project);
    }
}