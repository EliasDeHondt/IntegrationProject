using Business_Layer;
using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.platformModels;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class SharedPlatformsController : Controller
{

    private readonly SharedPlatformManager _sharedPlatformManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly UnitOfWork _uow;

    public SharedPlatformsController(SharedPlatformManager sharedPlatformManager, UserManager<IdentityUser> userManager, UnitOfWork uow)
    {
        _sharedPlatformManager = sharedPlatformManager;
        _userManager = userManager;
        _uow = uow;
    }


    [HttpGet]
    [Authorize(Roles = UserRoles.SystemAdmin)]
    public IActionResult GetAllSharedPlatforms()
    {
        var platforms = _sharedPlatformManager.GetAllSharedPlatforms();
        return Ok(platforms.Select(p => new SharedPlatformDto
        {
            Id = p.Id,
            OrganisationName = p.OrganisationName,
            Logo = p.Logo
        }));
    }
    
    [HttpPost]
    [Authorize(Roles = UserRoles.SystemAdmin)]
    public async Task<IActionResult> CreateSharedPlatform(CreatePlatformDto sharedPlatformDto)
    {
        _uow.BeginTransaction();
        var newPlatform = _sharedPlatformManager.AddSharedPlatform(sharedPlatformDto.OrganisationName, sharedPlatformDto.Logo);

        var user = new SpAdmin
        {
            Email = sharedPlatformDto.Email,
            UserName = sharedPlatformDto.Username,
            EmailConfirmed = true,
            SharedPlatform = newPlatform
        };

        await _userManager.CreateAsync(user, sharedPlatformDto.Password);
        await _userManager.AddToRoleAsync(user, UserRoles.PlatformAdmin);
        await _userManager.AddToRoleAsync(user, UserRoles.ProjectPermission);
        await _userManager.AddToRoleAsync(user, UserRoles.StatisticPermission);
        await _userManager.AddToRoleAsync(user, UserRoles.UserPermission);
        
        var platformDto = new SharedPlatformDto
        {
            Id = newPlatform.Id,
            OrganisationName = newPlatform.OrganisationName,
            Logo = newPlatform.Logo
        };
        _uow.Commit();
        return Created($"/api/SharedPlatforms/{platformDto.Id}", platformDto);
    }
    
}