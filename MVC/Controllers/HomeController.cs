/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.Diagnostics;
using Domain.Accounts;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    
    public IActionResult Index()
    {
        if (User.IsInRole(UserRoles.SystemAdmin)) return RedirectToAction("Dashboard", "SystemPlatform");
        if (User.IsInRole(UserRoles.PlatformAdmin)) return RedirectToAction("RedirectToDashboard", "SharedPlatform");
        if (User.IsInRole(UserRoles.Facilitator)) return RedirectToAction("Dashboard", "Facilitator");
       // return RedirectToPage("/Account/Login", new { area = "Identity"});
       return View();
    }

    public IActionResult PrivacyPolicy()
    {
        return View();
    }
    
    public IActionResult LegalGuidelines()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}