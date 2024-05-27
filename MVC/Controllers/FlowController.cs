/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Domain.Accounts;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize(Roles = UserRoles.Facilitator)]
    public IActionResult Step(long id)
    {
        try
        {
            var flow = _manager.GetFlowByIdWithTheme(id);
            return View(flow);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View("Error");
        }
    }
    
    [Authorize(Roles = UserRoles.Facilitator)]
    public IActionResult Facilitator()
    {
        return View();
    }
}