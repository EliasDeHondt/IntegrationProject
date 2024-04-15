using Business_Layer;
using Domain.Platform;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class SharedPlatformController : Controller
{

    private readonly SharedPlatformManager _sharedPlatformManager;
    
    public SharedPlatformController(SharedPlatformManager sharedPlatformManager)
    {
        _sharedPlatformManager = sharedPlatformManager;
    }
    
    
    public IActionResult Index()
    {
        var sharedPlatform = _sharedPlatformManager.GetSharedPlatformWithProjects(1);
        return View(sharedPlatform);
    }
}