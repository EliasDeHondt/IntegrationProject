using System.Security.Claims;
using Business_Layer;
using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize(policy: "userAccess")]
    public async Task<IActionResult> CreateFacilitator(FacilitatorViewModel model)
    {

        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPost("CreateAdmin")]
    [Authorize(policy: "userAccess")]
    public async Task<IActionResult> CreateAdmin(AdminViewModel model)
    {

        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPost("IsEmailInUse")]
    [Authorize]
    public bool IsEmailInUse(EmailDto email)
    {
        try
        {
            return _userManager.FindByEmailAsync(email.email).Result != null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return true;
        }
    }

    [HttpGet("IsUserInRole/{role}")]
    [Authorize]
    public bool IsUserInRole(string role)
    {
        try
        {
            return User.IsInRole(role);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    [HttpGet("GetUsersForPlatform/{platformId}")]
    [Authorize(policy: "admin")]
    public IActionResult GetUsersForPlatform(long platformId)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetUser/{email}")]
    [Authorize(policy: "admin")]
    public IActionResult GetUser(string email)
    {
        try
        {
            if (User.Identity is { IsAuthenticated: false }) return Unauthorized();
            var user = _userManager.FindByEmailAsync(email).Result!;
            return Ok(new UserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Permissions = _userManager.GetRolesAsync(user).Result
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpPut("UpdateUser/{email}")]
    [Authorize(policy: "userAccess")]
    public IActionResult UpdateUser(string email, UserViewModel model)
    {
        try
        {
            if (User.Identity is { IsAuthenticated: false }) return Unauthorized();
            var user = _userManager.FindByEmailAsync(email).Result!;
            user.Email = model.Email;
            user.UserName = model.UserName;
            _userManager.UpdateAsync(user);
            _userManager.RemoveFromRolesAsync(user, _userManager.GetRolesAsync(user).Result);
            _userManager.AddToRolesAsync(user, model.Permissions);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpPut("UpdateFacilitator/{email}")]
    [Authorize(policy: "userAccess")]
    public IActionResult UpdateFacilitator(string email, UpdateFacilitatorDto model)
    {
        try
        {
            if (User.Identity is { IsAuthenticated: false }) return Unauthorized();
            var user = _userManager.FindByEmailAsync(email).Result as Facilitator;
            user!.Email = model.Email;
            user.UserName = model.UserName;
            _userManager.UpdateAsync(user);

            _uow.BeginTransaction();
        
            foreach (var projectId in model.RemovedProjects)
            {
                var project = _projectManager.GetProject(projectId);
                _projectManager.DeleteProjectOrganizer(user, project);
            }
        
            foreach (var projectId in model.AddedProjects)
            {
                var project = _projectManager.GetProject(projectId);
                _projectManager.AddProjectOrganizer(user, project);
            }
        
            _uow.Commit();
        
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    
    [HttpGet("GetLoggedInEmail")]
    [Authorize]
    public IActionResult GetLoggedInEmail()
    {
        try
        {
            if (User.Identity is { IsAuthenticated: false }) return Unauthorized();
            return Ok(User.FindFirstValue(ClaimTypes.Email));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetPossibleProjectsForFacilitator/{email}")]
    [Authorize(policy: "userAccess")]
    public IActionResult GetPossibleProjectsForFacilitator(string email)
    {
        try
        {
            var projects = _projectManager.GetPossibleProjectsForFacilitator(email);
            return Ok(projects);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetAssignedProjectsForFacilitator/{email}")]
    [Authorize("flowAccess")]
    public IActionResult GetAssignedProjectsForFacilitator(string email)
    {
        try
        {
            var projects = _projectManager.GetAssignedProjectsForFacilitator(email);
            return Ok(projects);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpDelete("DeleteUser/{email}")]
    [Authorize(policy: "userAccess")]
    public IActionResult DeleteUser(string email)
    {
        try
        {
            if (User.Identity is { IsAuthenticated: false }) return Unauthorized();
            var user = _userManager.FindByEmailAsync(email).Result!;
            _userManager.DeleteAsync(user);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetLoggedInUser")]
    [Authorize]
    public async Task<IActionResult> GetLoggedInUser()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            string? email = user?.Email;
            string? userName = user?.UserName;
            UserViewModel model = new UserViewModel
            {
                Email = email,
                UserName = userName
            };
            return Ok(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
}