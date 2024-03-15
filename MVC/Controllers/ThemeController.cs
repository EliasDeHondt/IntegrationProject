/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class ThemeController : Controller
{
    private readonly ThemeManager _manager;

    public ThemeController(ThemeManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IActionResult MainThemes()
    {
        var mainThemes = _manager.GetAllMainThemes();
        return View(mainThemes);
    }
    
    public IActionResult Details(int id)
    {
        var mainTheme = _manager.GetMainThemeById(id);
        return View(mainTheme);
    }
}