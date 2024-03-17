/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class FlowController : Controller
{
    
    private FlowManager _manager;
    
    public FlowController(FlowManager manager)
    {
        _manager = manager;
    }
    
    public IActionResult Step(long id)
    {
        var flow = _manager.GetFlowByIdWithTheme(id);
        return View(flow);
    }
}