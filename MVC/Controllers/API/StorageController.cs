/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Google;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class StorageController : Controller
{
    
    private readonly CloudStorageOptions _options; // Google Cloud Storage Options
    private readonly UrlSigner _signer;

    public StorageController(CloudStorageOptions options)
    {
        _options = options;
        GoogleCredential credential = GoogleCredential.GetApplicationDefault();
        _signer = UrlSigner.FromCredential(credential);
    }
    
    [HttpGet("DownloadVideo/{videoName}")]
    public IActionResult DownloadVideo(string videoName)
    {
        try
        {
            string url = _signer.Sign(_options.BucketName, videoName, TimeSpan.FromDays(7), HttpMethod.Get);
            
            return Ok(url);
        }
        catch (GoogleApiException e) when (e.HttpStatusCode == System.Net.HttpStatusCode.NotFound) { return BadRequest(); }
    }

    [HttpPost("UploadVideo")]
    public async Task<IActionResult> UploadVideo(FileUpload file)
    {
        var client = await StorageClient.CreateAsync();
        var obj = await client.UploadObjectAsync(_options.BucketName, file.Name, file.Type, new MemoryStream(file.File));
        return Ok(obj.Name);
    }
    
}