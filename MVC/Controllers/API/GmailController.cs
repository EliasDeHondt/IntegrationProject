using Business_Layer;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class GmailController : Controller
{

    private readonly EmailManager _emailManager;
    
    public GmailController(EmailManager emailManager)
    {
        _emailManager = emailManager;
    }

    [HttpPost("SendEmail")]
    public IActionResult SendEmail(EmailModel emailDto)
    {
        _emailManager.SendEmail(emailDto.Subject, emailDto.Body, emailDto.To);
        return Ok();
    }
}