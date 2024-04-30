using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.Statistics;

public class StatisticsController: Controller
{
    public IActionResult Statistics()
    {
        return View();
    }
}