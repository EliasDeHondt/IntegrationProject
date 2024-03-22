using Google;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.API;


[ApiController]
[Route("api/[controller]")]
public class StorageController : Controller
{
    
    private readonly CloudStorageOptions _options; // Google Cloud Storage Options
    private readonly StorageClient _storage; // Google Cloud Storage Client
    private readonly UrlSigner _signer;

    public StorageController(CloudStorageOptions options)
    {
        _options = options;
        GoogleCredential credential = GoogleCredential.GetApplicationDefaultAsync().Result;
        _storage = StorageClient.Create(credential);
        _signer = UrlSigner.FromCredential(credential);
    }
    
    [HttpGet("DownloadVideo/{videoName}")]
    public IActionResult DownloadVideo(string videoName)
    {
            
        try
        {
            string url = _signer.Sign(_options.BucketName, videoName, TimeSpan.FromDays(15), HttpMethod.Get);
            
            return Ok(url);
        }
        catch (GoogleApiException e)
            when (e.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return BadRequest();
        }
    }
}