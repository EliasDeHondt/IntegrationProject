using Business_Layer;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class FlowsController : Controller
{
    private readonly FlowManager _manager;

    public FlowsController(FlowManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet("SetRespondentEmail/{flowId:int}/{inputEmail:string}")]
    public IActionResult SetRespondent(long flowId,string email)
    {
        _manager.SetParticipationByFlow(flowId,email);

        return Ok();
    }
}