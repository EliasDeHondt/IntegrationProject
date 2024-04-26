using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.Google;

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
    }


}