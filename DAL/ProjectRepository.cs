using Data_Access_Layer.DbContext;
using Domain.Accounts;
using Domain.FacilitatorFunctionality;
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
        return _ctx.Projects.Where(project => ids.Contains(project.Id)).ToList();
    }

    public Project ReadProject(long id)
    {
        return _ctx.Projects.Find(id);
    }
    
    public IEnumerable<Project> ReadAllProjectsForSharedPlatformIncludingMainTheme(long platformId)
    {
        return _ctx.Projects.Where(project => project.SharedPlatform.Id == platformId).Include(project => project.MainTheme).ToList();
    }
    
    public void CreateProjectOrganizer(ProjectOrganizer projectOrganizer)
    {
        if(_ctx.ProjectOrganizers.Where(organizer => organizer.Project.Id == projectOrganizer.Project.Id && organizer.Facilitator.Id == projectOrganizer.Facilitator.Id).ToList().Count == 0) _ctx.ProjectOrganizers.Add(projectOrganizer);
    }

    public IEnumerable<Project> ReadPossibleProjectsForFacilitator(string email)
    {
        var projects = _ctx.Projects.Include(p => p.MainTheme).ToList();
        var assignedProjects = ReadAssignedProjectsForFacilitator(email);
        
        return projects.Except(assignedProjects).ToList();
        
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

    public void RemoveProjectOrganizer(Facilitator user, Project project)
    {
        var projectOrganizer = _ctx.ProjectOrganizers.FirstOrDefault(po => po.Facilitator == user && po.Project == project);
        _ctx.ProjectOrganizers.Remove(projectOrganizer!);
    }
}