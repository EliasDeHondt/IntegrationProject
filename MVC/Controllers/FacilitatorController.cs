using System.Security.Claims;
using Business_Layer;
using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class FacilitatorController : Controller
{

    private readonly ProjectManager _projectManager;

    public FacilitatorController(ProjectManager projectManager)
    {
        _projectManager = projectManager;
    }
    
    [Authorize(Roles = UserRoles.Facilitator)]
    public IActionResult Dashboard()
    {
        try
        {
            var projects = _projectManager.GetAssignedProjectsForFacilitator(User.FindFirstValue(ClaimTypes.Email)!);
            return View(projects);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View("Error");
        }
    }
}