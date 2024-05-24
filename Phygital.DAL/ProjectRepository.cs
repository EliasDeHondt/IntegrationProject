using Data_Access_Layer.DbContext;
using Domain.Accounts;
using Domain.FacilitatorFunctionality;
using Domain.Platform;
using Domain.ProjectLogics;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer;

public class ProjectRepository
{
    private readonly CodeForgeDbContext _ctx;


    public ProjectRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }

    public IEnumerable<Project> ReadProjectsFromIds(IEnumerable<long> ids)
    {
        return _ctx.Projects
            .AsNoTracking()
            .Where(project => ids.Contains(project.Id))
            .ToList();
    }

    public Project ReadProject(long id)
    {
        return _ctx.Projects
            .Single(proj => proj.Id == id);
    }

    public IEnumerable<Project> ReadAllProjectsForSharedPlatformIncludingMainTheme(long platformId)
    {
        return _ctx.Projects
            .AsNoTracking()
            .Where(project => project.SharedPlatform.Id == platformId)
            .Include(project => project.MainTheme)
            .ToList();
    }

    public void CreateProjectOrganizer(ProjectOrganizer projectOrganizer)
    {
        if (_ctx.ProjectOrganizers.Count(organizer => organizer.Project.Id == projectOrganizer.Project.Id &&
                                                      organizer.Facilitator.Id == projectOrganizer.Facilitator.Id) ==
            0) _ctx.ProjectOrganizers.Add(projectOrganizer);
    }

    public IEnumerable<Project> ReadPossibleProjectsForFacilitator(string email)
    {
        var project = _ctx.Projects
            .Include(p => p.MainTheme)
            .Where(p => !_ctx.ProjectOrganizers.Any(po => po.Project.Id == p.Id && po.Facilitator.Email == email))
            .ToList();

        return project.ToList();
    }

    public IEnumerable<Project> ReadAssignedProjectsForFacilitator(string email)
    {
        return _ctx.ProjectOrganizers
            .Where(organizer => organizer.Facilitator.Email == email)
            .Include(organizer => organizer.Project)
            .ThenInclude(project => project.MainTheme)
            .Select(organizer => organizer.Project)
            .ToList();
    }

    public void DeleteProjectOrganizer(Facilitator user, Project project)
    {
        var projectOrganizer =
            _ctx.ProjectOrganizers.Single(po => po.Facilitator == user && po.Project == project);
        _ctx.ProjectOrganizers.Remove(projectOrganizer);
    }

    public void CreateProject(MainTheme mainTheme, SharedPlatform sharedPlatform, long id)
    {
        Project p = new Project(mainTheme.Subject, mainTheme, sharedPlatform, id);
        _ctx.Projects.Add(p);
        _ctx.SaveChanges();
    }

    public void CreateProject(Project project)
    {
        _ctx.Projects.Add(project);
        _ctx.SaveChanges();
    }

    public IEnumerable<Project> ReadProjectCount()
    {
        return _ctx.Projects;
    }

    public Project ReadProjectWithId(long id)
    {
        return _ctx.Projects.Find(id)!;
    }

    public Project ReadProjectIncludingSharedPlatformAndMainTheme(long id)
    {
        return _ctx.Projects
            .AsNoTracking()
            .Include(project => project.SharedPlatform)
            .Include(project => project.MainTheme)
            .Single(project => project.Id == id);
    }

    public void UpdateProject(long id, string title, string description)
    {
        var project = ReadProjectWithId(id);
        project.Title = title;
        project.Description = description;
    }

    public Project ReadProjectThroughMainTheme(long id)
    {
        return _ctx.Projects
            .AsNoTracking()
            .Include(project => project.MainTheme)
            .Single(project => project.MainTheme.Id == id);
    }

    public IEnumerable<Flow> ReadFlowsForProjectById(long projectId)
    {
        return _ctx.Flows.Where(project => project.Theme.Id == projectId); //todo
    }

    public Flow CreateFlowForProject(FlowType type, long themeId)
    {
        var theme = _ctx.MainThemes.Find(themeId)!;
        var flow = new Flow(type, theme);
        _ctx.Flows.Add(flow);

        return flow;
    }

    public IEnumerable<Flow> ReadNotesForProjectById(long id)
    {
        return _ctx.Projects
            .Where(p => p.Id == id)
            .Include(p => p.MainTheme)
            .ThenInclude(t => t.Themes)
            .ThenInclude(s => s.Flows)
            .ThenInclude(f => f.Steps)
            .ThenInclude(s => s.Notes)
            .SelectMany(p => p.MainTheme.Themes)
            .SelectMany(t => t.Flows)
            .AsEnumerable();
    }

    public bool UpdateProjectStyling(StylingTemplate template)
    {
        StylingTemplate? stylingTemplate = _ctx.StylingTemplates.Find(template.ProjectId);

        if (stylingTemplate == null)
        {
            Console.WriteLine("The styling template was not found.");
            return false;
        }

        stylingTemplate.ThemeName = template.ThemeName;
        stylingTemplate.CustomPrimaryColor = template.CustomPrimaryColor;
        stylingTemplate.CustomSecondaryColor = template.CustomSecondaryColor;
        stylingTemplate.CustomBackgroundColor = template.CustomBackgroundColor;
        stylingTemplate.CustomAccentColor = template.CustomAccentColor;

        _ctx.StylingTemplates.Update(stylingTemplate);
        
        return true;
    }

    public StylingTemplate? ReadStylingTemplate(long projectId)
    {
        StylingTemplate? template = _ctx.StylingTemplates.Find(projectId);
        if (template == null)
        {
            Console.WriteLine("Styling Template not found for projectId: " + projectId);
            return null;
        }
        
        return template;
    }
}