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
public class CreateFlowController : Controller
{
    private readonly FlowManager _manager;
    private readonly UnitOfWork _uow;

    public CreateFlowController(FlowManager manager, UnitOfWork uow)
    {
        _manager = manager;
        _uow = uow;
    }

    // GET
    public IActionResult FlowCreator()
    {
        return View();
    }

    //projectId does nothing for now -> setup for future issues.
    [HttpGet("GetFlows")]
    public IActionResult GetFlows()
    {
        var flows = _manager.GetAllFlows();

        return Ok(flows);
    }

    //projectId does nothing for now -> setup for future issues.
    [HttpPost("CreateFlow/{flowType}")]
    public IActionResult CreateFlow(string flowType)
    {

        FlowType type = Enum.Parse<FlowType>(flowType);
        
        _uow.BeginTransaction();
        
        Flow flow = _manager.Add(type);
        
        _uow.Commit();
        
        return Created("CreateFlow", flow);
    }
}