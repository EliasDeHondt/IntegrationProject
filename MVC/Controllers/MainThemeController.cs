/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class MainThemeController : Controller
{
    private readonly ThemeManager _manager;

    public MainThemeController(ThemeManager manager)
    {
        _manager = manager;
    }

    [Authorize(policy: "projectAccess")]
    public IActionResult MainTheme(long id)
    {
        try
        {
            var mainTheme = _manager.GetMainThemeById(id);
            return View(mainTheme);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View("Error");
        }
    }
}