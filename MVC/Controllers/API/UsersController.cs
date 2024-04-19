using Business_Layer;
using Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVC.Models.userModels;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ProjectManager _projectManager;
    private readonly UnitOfWork _uow;


    public UsersController(UserManager<IdentityUser> userManager, ProjectManager projectManager, UnitOfWork uow)
    {
        _userManager = userManager;
        _projectManager = projectManager;
        _uow = uow;
    }

    [HttpPost("CreateFacilitator")]
    public async Task<IActionResult> CreateFacilitator(FacilitatorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var facilitator = new Facilitator
        {
            UserName = model.Name,
            Email = model.Email,
            Projects = _projectManager.GetAllProjectsFromIds(model.ProjectIds).ToList()
        };
        
        _uow.BeginTransaction();
        var result = await _userManager.CreateAsync(facilitator, model.Password);
        
        await _userManager.AddToRoleAsync(facilitator, UserRoles.Facilitator);
        
        if(!model.ProjectIds.IsNullOrEmpty()) _projectManager.AddFacilitatorToProjects(facilitator, model.ProjectIds.ToArray());
        
        _uow.Commit();
        
        return Created("CreateFacilitator", result);
    }
    
    
}