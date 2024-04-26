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
    
    public IActionResult Projects()
    {
        var project = _manager.GetProject(1);
        return View(project);
    }
    
}