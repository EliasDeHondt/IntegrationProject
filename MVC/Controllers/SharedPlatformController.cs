using System.Security.Claims;
using Business_Layer;
using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class SharedPlatformController : Controller
{

    private readonly SharedPlatformManager _sharedPlatformManager;
    private readonly CustomUserManager _customUserManager;
    
    public SharedPlatformController(SharedPlatformManager sharedPlatformManager, CustomUserManager customUserManager)
    {
        _sharedPlatformManager = sharedPlatformManager;
        _customUserManager = customUserManager;
    }
    
    [Authorize(policy: "admin")]
    public IActionResult Dashboard()
    {
        SpAdmin admin = _customUserManager.GetPlatformAdminWithSharedPlatform(User.FindFirstValue(ClaimTypes.Email));
        var sharedPlatform = _sharedPlatformManager.GetSharedPlatformWithProjects(admin.SharedPlatform.Id);
        return View(sharedPlatform);
    }
}