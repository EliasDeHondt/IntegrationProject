/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class StepsController : Controller
{
    private readonly StepManager _manager;
    private readonly CloudStorageOptions _options;
    private readonly StorageClient _storage;

    public StepsController(StepManager manager, CloudStorageOptions options)
    {
        _manager = manager;
        _options = options;
        
        GoogleCredential credential = GoogleCredential.GetApplicationDefaultAsync().Result;
        _storage = StorageClient.Create(credential);
    }

    [HttpGet("GetNextStep/{flowId:int}/{stepNumber:long}")]
    public ActionResult GetNextStep(int stepNumber, long flowId)
    {
        StepBase stepBase = _manager.GetStepForFlowByNumber(flowId, stepNumber);
        
        switch (stepBase)
        {
            case CombinedStep cStep:
            {
                CombinedStepViewModel stepViewModel = CreateCominedStepViewModel(cStep);
                return Ok(stepViewModel);
            }
            case InformationStep iStep:
            {
                InformationStepViewModel stepViewModel = CreateInformationStepViewModel(iStep);

                return Ok(stepViewModel);
            }
            case null:
                return NoContent();
            default:
                return Ok(stepBase);
        }
    }

    private async Task<InformationViewModel> CreateInformationViewModel(InformationBase information)
    {
        if (typeof(InformationBase) == typeof(Video))
        {
            _options.ObjectName = information.GetInformation();
            if (new[] { null, "", "codeforge-bucket-videos" }.Contains(_options.BucketName)) return new InformationViewModel();
            
            try
            {
                await using MemoryStream m = new();
                string filePath = $"/wwwroot/Assets/Videos/{_options.ObjectName}";
                await using FileStream fs = System.IO.File.Create(filePath);
                await _storage.DownloadObjectAsync(_options.BucketName, _options.ObjectName, m);
                m.CopyTo(fs);
            }
            catch (GoogleApiException e)
                when (e.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Does not exist yet.  No problem.
            }
            
        }
        
        
        return new InformationViewModel
        {
            Id = information.Id,
            Information = information.GetInformation(),
            InformationType = information.GetType().Name
        };
    }

    private CombinedStepViewModel CreateCominedStepViewModel(CombinedStep step)
    {
        return new CombinedStepViewModel
        {
            Id = step.Id,
            InformationViewModel = CreateInformationViewModel(step.InformationBase).Result,
            StepNumber = step.StepNumber
        };
    }
    
    private InformationStepViewModel CreateInformationStepViewModel(InformationStep step)
    {
        return new InformationStepViewModel
        {
            Id = step.Id,
            InformationViewModel = CreateInformationViewModel(step.InformationBase).Result,
            StepNumber = step.StepNumber
        };
    }
    
}