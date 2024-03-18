using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class WebcamController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}