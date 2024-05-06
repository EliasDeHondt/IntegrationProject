using Business_Layer;
using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.platformModels;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class SharedPlatformsController : Controller
{

    private readonly SharedPlatformManager _sharedPlatformManager;

    public SharedPlatformsController(SharedPlatformManager sharedPlatformManager)
    {
        _sharedPlatformManager = sharedPlatformManager;
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
}