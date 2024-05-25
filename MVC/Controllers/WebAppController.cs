using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

[Route("WebApp")]
public class WebAppController : Controller
{
    
    [Route("Feed")]
    public IActionResult Feed()
    {
        return View(null);
    }

    [Route("Feed/{id}")]
    public IActionResult Feed(long id)
    {
        return View(id);
    }
    
}