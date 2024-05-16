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

    [Authorize(Roles = UserRoles.ProjectPermission)]
    public IActionResult MainTheme(long id)
    {
        var mainTheme = _manager.GetMainThemeById(id);
        return View(mainTheme);
    }
}