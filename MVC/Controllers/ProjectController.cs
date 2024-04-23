using System.Diagnostics;
using Business_Layer;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class ProjectController: Controller
{
    private ProjectManager _manager;
    
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