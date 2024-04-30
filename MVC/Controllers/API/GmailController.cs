using System.Net.Mail;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.Google;

[ApiController]
[Route("api/[controller]")]
public class GmailController : Controller
{

    private readonly GmailService _service;

    public GmailController()
    {
        GoogleCredential credential = GoogleCredential.GetApplicationDefault();
        _service = new GmailService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "CodeForge"
        });

        var msg = new Message();

        msg.Id = "me";
        msg.Raw = "test";

        
    }

    [Route("SendEmail")]
    public IActionResult SendEmail(EmailModel emailDto)
    {
        var message = new MailMessage()
        {
            From = new MailAddress(emailDto.From),
            Subject = emailDto.Subject,
            IsBodyHtml = true,
            Body = emailDto.Body
        };
        message.To.Add(new MailAddress(emailDto.To));
        return Ok();
    }

}