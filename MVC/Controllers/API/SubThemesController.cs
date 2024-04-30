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
public class SubThemesController : ControllerBase
{
    private readonly ThemeManager _manager;
    private readonly UnitOfWork _uow;

    public SubThemesController(ThemeManager manager, UnitOfWork uow)
    {
        _manager = manager;
        _uow = uow;
    }

    [HttpGet("{id}/Flows")]
    public IActionResult GetFlowsOfSubTheme(long id)
    {
        var flows = _manager.GetFlowsOfSubThemeById(id);

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

    [HttpPost("AddSubTheme")]
    public IActionResult AddSubTheme(SubThemeViewModel dto)
    {
        _uow.BeginTransaction();
        var theme = _manager.AddSubTheme(dto.Subject, dto.MainThemeId);
        _uow.Commit();
        return CreatedAtAction("AddSubTheme", theme);
    }
    
}