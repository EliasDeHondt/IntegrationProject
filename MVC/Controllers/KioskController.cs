using Business_Layer;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class KioskController : Controller
{
    private readonly FlowManager _manager;

    public KioskController(FlowManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        return View();
    }
}