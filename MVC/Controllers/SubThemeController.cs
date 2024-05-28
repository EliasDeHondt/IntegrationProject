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

public class SubThemeController : Controller
{
    private readonly ThemeManager _manager;

    public SubThemeController(ThemeManager manager)
    {
        _manager = manager;
    }
    
    [Authorize(policy: "projectAccess")]
    public IActionResult SubTheme(long id)
    {
        try
        {
            var subTheme = _manager.GetSubThemeByIdWithMainThemeAndProject(id);
            return View(subTheme);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View("Error");
        }
    }
}