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
    
    [Authorize(policy: "projectAccess")]
    public IActionResult ProjectPage(long id, bool isMainThemeId = false)
    {
        try
        {
            var project = isMainThemeId ? _manager.GetProjectThroughMainTheme(id) : _manager.GetProject(id);
            return View(project);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View("Error");
        }
    }
    
    [Authorize(policy: "projectAccess")]
    public IActionResult Notes(long id)
    {
        try
        {
            var project = _manager.GetProject(id);
            return View(project);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View("Error");
        }
    }
}