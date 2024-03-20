/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

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