using System.Security.Claims;
using Business_Layer;
using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

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
    public IActionResult Dashboard(long id)
    {
        try
        {
            long platId = id;
            if (platId == 0)
            {
                SpAdmin admin = _customUserManager.GetPlatformAdminWithSharedPlatform(User.FindFirstValue(ClaimTypes.Email));
                platId = admin.SharedPlatform.Id;
            }
            var sharedPlatform = _sharedPlatformManager.GetSharedPlatformWithProjects(platId);
            return View(sharedPlatform);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View("Error", new ErrorViewModel
            {
                RequestId = e.Source
            });
        }
    }

    [Authorize(policy: "admin")]
    public IActionResult RedirectToDashboard()
    {
        try
        {
            SpAdmin admin = _customUserManager.GetPlatformAdminWithSharedPlatform(User.FindFirstValue(ClaimTypes.Email));
            var id = admin.SharedPlatform.Id;
            return RedirectToAction("Dashboard", new {id});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View("Error", new ErrorViewModel
            {
                RequestId = e.Source
            });
        }
    }
    
}