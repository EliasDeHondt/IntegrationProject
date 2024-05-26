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

    private long GetPlatformId()
    {
        SpAdmin admin = _customUserManager.GetPlatformAdminWithSharedPlatform(User.FindFirstValue(ClaimTypes.Email));
        return admin.SharedPlatform.Id;
    }
    
    [Authorize(policy: "admin")]
    public IActionResult Dashboard()
    {
        long id = GetPlatformId();
        var sharedPlatform = _sharedPlatformManager.GetSharedPlatformWithProjects(id);
        return View(sharedPlatform);
    }

    [Authorize(policy: "admin")]
    public IActionResult RedirectToDashboard()
    {
        long id = GetPlatformId();
        return RedirectToAction("Dashboard", new {id});
    }
    
}