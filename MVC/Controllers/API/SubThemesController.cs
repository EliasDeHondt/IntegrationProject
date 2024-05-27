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
    [Authorize(policy: "flowAccess")]
    public IActionResult GetFlowsOfSubTheme(long id)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPost("AddSubTheme")]
    [Authorize(policy: "projectAccess")]
    public IActionResult AddSubTheme(SubThemeViewModel dto)
    {
        try
        {
            _uow.BeginTransaction();
            var theme = _manager.AddSubTheme(dto.Subject, dto.MainThemeId);
            _uow.Commit();
            return CreatedAtAction("AddSubTheme", theme);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetSubthemesForProject/{id}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult GetSubthemesForProject(long id)
    {
        try
        {
            var subThemes = _manager.GetSubthemesForProject(id);
            var subThemeDtos = subThemes.Select(subTheme => new SubThemeViewModel { Id = subTheme.Id, Subject = subTheme.Subject, MainThemeId = subTheme.MainTheme.Id }).ToList();
            return Ok(subThemeDtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
 
    [HttpPut("UpdateSubTheme/{id}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult UpdateSubTheme(long id, SubThemeViewModel dto)
    {
        try
        {
            _uow.BeginTransaction();
            _manager.UpdateSubTheme(id, dto.Subject);
            _uow.Commit();
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpPost("CreateSubthemeFlow/{flowType}/{themeId}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult CreateFlow(string flowType,int themeId)
    {

        try
        {
            FlowType type = Enum.Parse<FlowType>(flowType);
        
            _uow.BeginTransaction();
        
            Flow flow = _manager.CreateFlowForSub(type, themeId);
        
            _uow.Commit();
        
            return Created("CreateFlow", flow);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpDelete("DeleteSubTheme/{id:long}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult DeleteSubTheme(long id)
    {
        try
        {
            _uow.BeginTransaction();
        
            _manager.DeleteSubTheme(id);
        
            _uow.Commit();

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

}