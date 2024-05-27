using Business_Layer;
using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class KioskController : Controller
{
    private readonly FlowManager _manager;

    public KioskController(FlowManager manager)
    {
        _manager = manager;
    }

    [Authorize(Roles = UserRoles.Facilitator)]
    public IActionResult Index(long projectId)
    {
        return View(projectId);
    }
}