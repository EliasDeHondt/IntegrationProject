/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Domain.ProjectLogics;
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
    public ActionResult GetMainThemes()
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

    [HttpGet("{id}/SubThemes")]
    public ActionResult GetSubThemesOfMainTheme(long id)
    {
        var subThemes = _manager.GetSubThemesOfMainThemeById(id);

        if (!subThemes.Any())
            return NoContent();

        return Ok(subThemes.Select(subtheme => new SubThemeViewModel()
        {
            Id = subtheme.Id,
            Subject = subtheme.Subject,
            Flows = subtheme.Flows,
            MainTheme = subtheme.MainTheme.Id
        }));
    }
}