using System.Security.Claims;
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
    private readonly SharedPlatformManager _platformManager;
    private readonly UnitOfWork _uow;


    public UsersController(UserManager<IdentityUser> userManager, SharedPlatformManager platformManager,
        ProjectManager projectManager, UnitOfWork uow)
    {
        _userManager = userManager;
        _projectManager = projectManager;
        _platformManager = platformManager;
        _uow = uow;
    }

    [HttpPost("CreateFacilitator")]
    public async Task<IActionResult> CreateFacilitator(FacilitatorViewModel model)
    {
        
        if (User.Identity is { IsAuthenticated: false }) return Unauthorized();
        
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        _uow.BeginTransaction();
        var facilitator = new Facilitator
        {
            UserName = model.Name,
            Email = model.Email,
            EmailConfirmed = true,
            SharedPlatformId = model.platformId
        };

        foreach (var projectId in model.ProjectIds)
        {
            var project = _projectManager.GetProject(projectId);
            _projectManager.AddProjectOrganizer(facilitator, project);
        }


        var result = await _userManager.CreateAsync(facilitator, model.Password);

        await _userManager.AddToRoleAsync(facilitator, UserRoles.Facilitator);

        _uow.Commit();

        return Created("CreateFacilitator", result);
    }

    [HttpPost("CreateAdmin")]
    public async Task<IActionResult> CreateAdmin(AdminViewModel model)
    {
        
        if (User.Identity is { IsAuthenticated: false }) return Unauthorized();
        if (!User.IsInRole(UserRoles.UserPermission)) return Forbid();
        
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var admin = new SpAdmin
        {
            UserName = model.Name,
            Email = model.Email,
            EmailConfirmed = true,
            SharedPlatform = _platformManager.GetSharedPlatform(model.PlatformId)
        };
        
        _uow.BeginTransaction();
        var result = await _userManager.CreateAsync(admin, model.Password);
        await _userManager.AddToRoleAsync(admin, UserRoles.PlatformAdmin);
        foreach (var permission in model.Permissions)
        {
            await _userManager.AddToRoleAsync(admin, permission);
        }
        _uow.Commit();

        return Created("CreateAdmin", result);
    }

    [HttpPost("IsEmailInUse")]
    public bool IsEmailInUse(EmailDto email)
    {
        return _userManager.FindByEmailAsync(email.email).Result != null;
    }

    [HttpGet("IsUserInRole/{role}")]
    public bool IsUserInRole(string role)
    {
        return User.IsInRole(role);
    }
    
    [HttpGet("GetUsersForPlatform/{platformId}")]
    public IActionResult GetUsersForPlatform(long platformId)
    {
        if (User.Identity is { IsAuthenticated: false }) return Unauthorized();
        var users = _platformManager.GetUsersForPlatform(platformId);

        ICollection<UserViewModel> userList = new List<UserViewModel>();
        foreach (var user in users)
        {
            userList.Add(new UserViewModel
            {
                UserName = user.UserName,
                Email = user.Email
            });
        }
        
        return Ok(userList);
    }
    
}