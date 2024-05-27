using Business_Layer;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.projectModels;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : Controller
{

    private readonly ProjectManager _projectManager;
    private readonly SharedPlatformManager _sharedPlatformManager;
    private readonly UnitOfWork _uow;

    public ProjectsController(ProjectManager projectManager, SharedPlatformManager sharedPlatformManager, UnitOfWork uow)
    {
        _projectManager = projectManager;
        _sharedPlatformManager = sharedPlatformManager;
        _uow = uow;
    }

    [HttpGet("GetProjectsForSharedPlatform/{id}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult GetProjectsForSharedPlatform(long id)
    {
        try
        {
            return Ok(_projectManager.GetAllProjectsForSharedPlatformWithMainTheme(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPost("AddProject")]
    [Authorize(policy: "projectAccess")]
    public IActionResult AddProject(ProjectDto model)
    {
        try
        {
            _uow.BeginTransaction();
        
            var project = new Project
            {
                Title = model.Title,
                Description = model.Description,
                SharedPlatform = _sharedPlatformManager.GetSharedPlatform(model.SharedPlatformId)
            };
        
            // If the logo is also added (Image is not null)
            if (model.Image != null) project.Image = model.Image;

            _projectManager.CreateProject(project);
            _sharedPlatformManager.AddProjectToPlatform(project, model.SharedPlatformId);
        
            _uow.Commit();

            return Created("CreateProject",  project);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpPut("UpdateProject/{projectId}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult UpdateProject(long projectId, ProjectDto model)
    {
        try
        {
            _uow.BeginTransaction();
            _projectManager.ChangeProject(projectId, model.Title, model.Description);
            _uow.Commit();
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetProjectsForPlatform/{platformId}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult GetProjectsForPlatform(long platformId)
    {
        try
        {
            if (User.Identity is { IsAuthenticated: false }) return Unauthorized();
            var projects = _sharedPlatformManager.GetProjectsForPlatform(platformId);

            ICollection<ProjectViewModel> projectList = new List<ProjectViewModel>();
            foreach (var project in projects)
            {
                projectList.Add(new ProjectViewModel
                {
                    Id = project.Id,
                    Name = project.Title,
                    Description = project.Description
                });
            }

            return Ok(projectList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetProjectWithId/{projectId}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult GetProjectWithId(long projectId)
    {
        try
        {
            Project project = _projectManager.GetProjectWithSharedPlatformAndMainTheme(projectId);

            if (project == null)
            {
                return NotFound();
            }
        
            ProjectDto projectDto = new ProjectDto
            {
                MainThemeId = project.MainTheme.Id,
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                SharedPlatformId = project.SharedPlatform.Id
            };

            return Ok(projectDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetFlowsForProject/{projectId}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult GetFlowsForProject(long projectId)
    {
        try
        {
            var flows = _projectManager.GetFlowsForProjectById(projectId);

            return Ok(flows);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }

    }
    
    [HttpPost("CreateProjectFlow/{flowType}/{themeId}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult CreateFlow(string flowType,long themeId)
    {

        try
        {
            FlowType type = Enum.Parse<FlowType>(flowType);
        
            _uow.BeginTransaction();
        
            Flow flow = _projectManager.CreateFlowForProject(type, themeId);
        
            _uow.Commit();
        
            return Created("CreateFlow", flow);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpGet("GetNotesForProject/{projectId}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult GetNotesForProject(long projectId)
    {
        try
        {
            var flows = _projectManager.GetNotesForProjectById(projectId);

            return Ok(flows.Select(flow => new FlowViewModel
            {
                Id = flow.Id,
                FlowType = flow.FlowType,
                Steps = flow.Steps,
                Participations = flow.Participations,
                ThemeId = flow.Theme.Id
            }));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpPut("UpdateProjectClosed/{projectId}/{closeProject}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult UpdateProjectClosed(long projectId,bool closeProject)
    {
        try
        {
            _uow.BeginTransaction();

            _projectManager.UpdateProjectClosed(projectId, closeProject);
        
            _uow.Commit();

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetProjectClosed/{projectId}")]
    [Authorize(policy: "projectAccess")]
    public IActionResult GetProjectClosed(long projectId)
    {
        try
        {
            _uow.BeginTransaction();

            bool isClosed = _projectManager.GetProjectClosed(projectId);
        
            _uow.Commit();

            return Ok(isClosed);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}