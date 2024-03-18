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

    public StorageController(CloudStorageOptions options)
    {
        _options = options;
        GoogleCredential credential = GoogleCredential.GetApplicationDefaultAsync().Result;
        _storage = StorageClient.Create(credential);
    }
    
    [HttpGet("DownloadVideo/{videoName}")]
    public IActionResult DownloadVideo(string videoName)
    {
            
        try
        {
            using MemoryStream m = new();
            string filePath = $"./wwwroot/Assets/Videos/{videoName}";
            _storage.GetBucket(_options.BucketName); 
            _storage.DownloadObject(_options.BucketName, videoName, m);
            m.Position = 0;

            using FileStream fs = new(filePath, FileMode.Create); 
            m.CopyTo(fs);
            
            return Ok(filePath.Replace("./wwwroot", ""));
        }
        catch (GoogleApiException e)
            when (e.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return BadRequest();
        }
    }
}