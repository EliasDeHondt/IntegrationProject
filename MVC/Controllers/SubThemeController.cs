using Business_Layer;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class SubThemeController : Controller
{
    private readonly ThemeManager _manager;

    public SubThemeController(ThemeManager manager)
    {
        _manager = manager;
    }
    
    public IActionResult SubTheme(long id)
    {
        var subTheme = _manager.GetSubThemeByIdWithMainTheme(id);
        return View(subTheme);
    }
}