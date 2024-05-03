using Business_Layer;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.mailModels;

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

    [HttpPost("SendNewUserEmail")]
    public IActionResult SendNewUserEmail(NewUserEmailModel emailDto)
    {
        string html = System.IO.File.ReadAllText("./EmailHTMLS/CodeForge-NewUser.html");
        html = html.Replace("[EMPLOYEEEMAIL]", emailDto.To);
        html = html.Replace("[EMPLOYEEPASSWORD]", emailDto.Password);
        html = html.Replace("[LINK]", "https://codeforge.eliasdh.com/Identity/Account/Login");
        
        _emailManager.SendEmail("New CodeForge User", html, emailDto.To);
        return Ok();
    }
    
    
}