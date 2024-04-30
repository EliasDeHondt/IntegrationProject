using Business_Layer;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class EditFlowsController : Controller
{
    private readonly FlowManager _manager;
    private readonly UnitOfWork _uow;

    public EditFlowsController(FlowManager manager, UnitOfWork uow)
    {
        _manager = manager;
        _uow = uow;
    }

    [HttpGet("/EditFlows/GetSteps/{flowId:long}")]
    public IActionResult GetSteps(long flowId)
    {
        var steps = _manager.GetAllStepsInFlow(flowId);

        return Ok(steps);
    }

}