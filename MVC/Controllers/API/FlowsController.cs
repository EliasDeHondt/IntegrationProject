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
    
    [HttpPost("SetRespondentEmail/{flowId:int}/{inputEmail}")]
    public IActionResult SetRespondentEmail(int flowId, string inputEmail)
    {
        if (inputEmail == null)
        {
            return BadRequest();
        }
        
        _manager.SetParticipationByFlow(flowId, inputEmail);
        
        return Ok();
        
    }
}