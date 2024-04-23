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
        return _ctx.Projects.Where(project => ids.Contains(project.Id));
    }

    public Project ReadProject(long id)
    {
        return _ctx.Projects.Find(id);
    }
    
    public IEnumerable<Project> ReadAllProjectsForSharedPlatformIncludingMainTheme(long platformId)
    {
        return _ctx.Projects.Where(project => project.SharedPlatform.Id == platformId).Include(project => project.MainTheme);
    }
    
    public void CreateProjectOrganizer(ProjectOrganizer projectOrganizer)
    {
        if(_ctx.ProjectOrganizers.Where(organizer => organizer.Project.Id == projectOrganizer.Project.Id && organizer.Facilitator.Id == projectOrganizer.Facilitator.Id).ToList().Count == 0) _ctx.ProjectOrganizers.Add(projectOrganizer);
    }

    public void CreateProject(MainTheme mainTheme,SharedPlatform sharedPlatform,long id)
    {
        Project p = new Project(mainTheme,sharedPlatform,id);
        _ctx.Projects.Add(p);
        _ctx.SaveChanges();
    }
}