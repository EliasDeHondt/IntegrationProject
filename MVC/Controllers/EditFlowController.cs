using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers;

using Business_Layer;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Mvc;

public class EditFlowController : Controller
{
    [Authorize(Roles = UserRoles.ProjectPermission)]
    public IActionResult FlowEditor()
    {
        return View();
    }
}