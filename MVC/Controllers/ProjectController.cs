using Business_Layer;
using Domain.Accounts;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.stylingModels;

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
    
    [Authorize(Roles = UserRoles.ProjectPermission)]
    public IActionResult Notes(long id)
    {
        var project = _manager.GetProject(id);
        return View(project);
    }
    
    [HttpGet("/Project/GetStylingTemplate/{projectId:long}")]
    public IActionResult GetStylingTemplate(long projectId)
    {
        StylingTemplate? stylingTemplate = _manager.ReadStylingTemplate(projectId);
        if (stylingTemplate != null)
        {
            return Json(stylingTemplate);
        }
        return NotFound();
    }
    
    [HttpGet("/Project/Styling/{projectId}")]
    public IActionResult Styling(string projectId)
    {
        ViewData["ProjectId"] = projectId;
        return View();
    }
    
    [HttpPost]
    public IActionResult SaveCustomColors(StylingViewModel model)
    {
        //TODO: Remove this when done
        Console.WriteLine(model.ProjectId);
        Console.WriteLine(model.ThemeName);
        Console.WriteLine(model.CustomPrimaryColor);
        Console.WriteLine(model.CustomSecondaryColor);
        Console.WriteLine(model.CustomBackgroundColor);
        Console.WriteLine(model.CustomAccentColor);
        if (ModelState.IsValid)
        {
            StylingTemplate template = new StylingTemplate(
                model.ProjectId,
                model.ThemeName,
                model.CustomPrimaryColor,
                model.CustomSecondaryColor,
                model.CustomBackgroundColor,
                model.CustomAccentColor
                );
            
            bool result = _manager.UpdateStylingSettings(template);
            if (!result)
            {
                ModelState.AddModelError("", "There was an error saving the styling settings.");
                return BadRequest("An error occurred with the provided data.");
            }
        }
        else
        {
            Console.WriteLine("The given model is invalid.");
        }
        
        return Ok("Colors updated succesfully");
    }
}