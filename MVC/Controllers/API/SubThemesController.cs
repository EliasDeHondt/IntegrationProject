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
public class SubThemesController : ControllerBase
{
    private readonly ThemeManager _manager;
    private readonly FlowManager _flowmanager;
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
    
    [HttpGet("GetSubthemesForProject/{id}")]
    public IActionResult GetSubthemesForProject(long id)
    {
        var subThemes = _manager.GetSubthemesForProject(id);
        var subThemeDtos = subThemes.Select(subTheme => new SubThemeViewModel { Id = subTheme.Id, Subject = subTheme.Subject, MainThemeId = subTheme.MainTheme.Id }).ToList();
        return Ok(subThemeDtos);
    }
 
    [HttpPut("UpdateSubTheme/{id}")]
    public IActionResult UpdateSubTheme(long id, SubThemeViewModel dto)
    {
        _uow.BeginTransaction();
        _manager.UpdateSubTheme(id, dto.Subject);
        _uow.Commit();
        return NoContent();
    }
    
    [HttpPost("CreateSubthemeFlow/{flowType}/{themeId}")]
    public IActionResult CreateFlow(string flowType,int themeId)
    {

        FlowType type = Enum.Parse<FlowType>(flowType);
        
        _uow.BeginTransaction();
        
        Flow flow = _manager.CreateFlowForSub(type, themeId);
        
        _uow.Commit();
        
        return Created("CreateFlow", flow);
    }

    [HttpDelete("DeleteSubTheme/{id:long}")]
    public IActionResult DeleteSubTheme(long id)
    {
        _uow.BeginTransaction();
        
        _manager.DeleteSubTheme(id);
        
        _uow.Commit();

        return NoContent();
    }

    [HttpGet("GetProjectId/{themeId:long}")]
    public IActionResult GetProjectId(long themeId)
    {
        
        long? projectId = _manager.ReadProjectId(themeId);

        if (projectId == null)
        {
            return NotFound();
        }

        return Ok(projectId);
    }

}