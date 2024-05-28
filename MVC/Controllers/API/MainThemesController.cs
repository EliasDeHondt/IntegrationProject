/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class MainThemesController : ControllerBase
{
    private readonly ThemeManager _manager;

    public MainThemesController(ThemeManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    [Authorize(policy: "flowAccess")]
    public ActionResult GetMainThemes()
    {
        try
        {
            var mainThemes = _manager.GetAllMainThemes();

            if (!mainThemes.Any())
                return NoContent();

            return Ok(mainThemes.Select(mainTheme => new MainThemeViewModel
            {
                Id = mainTheme.Id,
                Subject = mainTheme.Subject,
                Flows = mainTheme.Flows,
                Themes = mainTheme.Themes
            }));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("{id}/SubThemes")]
    [Authorize(policy: "flowAccess")]
    public ActionResult GetSubThemesOfMainTheme(long id)
    {
        try
        {
            var subThemes = _manager.GetSubThemesOfMainThemeById(id);

            if (!subThemes.Any())
                return NoContent();

            return Ok(subThemes.Select(subtheme => new SubThemeViewModel()
            {
                Id = subtheme.Id,
                Subject = subtheme.Subject,
                Flows = subtheme.Flows,
                MainThemeId = subtheme.MainTheme.Id
            }));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}