using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class SystemPlatformController : Controller
{
    
    [Authorize(Roles = UserRoles.SystemAdmin)]
    public IActionResult Dashboard()
    {
        return View();
    }
}