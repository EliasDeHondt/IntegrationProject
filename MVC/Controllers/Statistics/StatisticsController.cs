using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.Statistics;

public class StatisticsController: Controller
{
    [Authorize(policy: "admin")]
    public IActionResult Statistics()
    {
        return View();
    }
}