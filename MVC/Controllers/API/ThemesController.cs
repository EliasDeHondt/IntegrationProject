/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class ThemesController : ControllerBase
{
    private readonly ThemeManager _manager;

    public ThemesController(ThemeManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public ActionResult AllMainThemes()
    {
        var allMainThemes = _manager.GetAllMainThemes();

        if (!allMainThemes.Any())
        {
            return NoContent();
        }

        return Ok(allMainThemes.Select(mainTheme => new MainThemeViewModel
        {
            Id = mainTheme.Id,
            Subject = mainTheme.Subject,
            Flows = mainTheme.Flows,
            Themes = mainTheme.Themes
        }));
    }

    [HttpGet("{mainThemeId:long}/SubThemes")]
    public IActionResult GetSubThemes(long mainThemeId)
    {
        var subThemes = _manager.GetSubThemesOfMainThemeById(mainThemeId);

        if (subThemes == null || !subThemes.Any())
            return NoContent();

        List<SubThemeViewModel> subThemeViewModels = new List<SubThemeViewModel>();

        foreach (var subTheme in subThemes)
        {
            subThemeViewModels.Add(new SubThemeViewModel()
            {
                Id = subTheme.Id,
                Flows = subTheme.Flows,
                Subject = subTheme.Subject
            });
        }

        return Ok(subThemeViewModels);
    }
}