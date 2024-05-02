namespace MVC.Controllers;

using Business_Layer;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Mvc;

public class EditFlowController : Controller
{
    public IActionResult FlowEditor()
    {
        return View();
    }
}