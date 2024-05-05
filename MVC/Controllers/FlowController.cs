/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MVC.Controllers;

public class FlowController : Controller
{
    
    private readonly FlowManager _manager;
    
    public FlowController(FlowManager manager)
    {
        _manager = manager;
    }
    
    public IActionResult Step(long id)
    {
        var flow = _manager.GetFlowByIdWithTheme(id);
        return View(flow);
    }
    
    public IActionResult Facilitator()
    {
        return View();
    }

    public PartialViewResult FlowPicker(string type)
    {
        return PartialView("Partials/_FlowPickerPartial", type);
    }
}