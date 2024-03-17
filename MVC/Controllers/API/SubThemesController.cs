using Business_Layer;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class SubThemesController : ControllerBase
{
    private readonly ThemeManager _manager;

    public SubThemesController(ThemeManager manager)
    {
        _manager = manager;
    }

    [HttpGet("{subThemeId}/Flows")]
    public ActionResult GetSubThemesOfMainTheme(long mainThemeId, long subThemeId)
    {
        var flows = _manager.GetFlowsOfSubThemeById(subThemeId);

        if (!flows.Any())
            return NoContent();

        return Ok(flows.Select(flow => new FlowViewModel
        {
            Id = flow.Id,
            FlowType = flow.FlowType,
            Steps = flow.Steps,
            Participations = flow.Participations
        }));
    }
}