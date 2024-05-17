using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class WebAppController : Controller
{
    
    public IActionResult Feed()
    {
        return View();
    }
}