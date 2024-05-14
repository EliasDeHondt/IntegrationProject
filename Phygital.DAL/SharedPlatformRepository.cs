using Data_Access_Layer.DbContext;
using Domain.Platform;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer;

public class SharedPlatformRepository
{
    private readonly CodeForgeDbContext _ctx;
    
    public SharedPlatformRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }

    public SharedPlatform ReadSharedPlatformIncludingProjects(long id)
    {
        return _ctx.SharedPlatforms
            .AsNoTracking()
            .Include(platform => platform.Projects)
            .Single(platform => platform.Id == id);
    }

    public SharedPlatform ReadSharedPlatform(long id)
    {
        return _ctx.SharedPlatforms
            .Single(platform => platform.Id == id);
    }

    public IEnumerable<IdentityUser> ReadUsersForPlatform(long id)
    {
        var facilitators = _ctx.SharedPlatforms
            .AsNoTracking()
            .Include(platform => platform.Faciliators)
            .Single(platform => platform.Id == id)
            .Faciliators;

        var admins = _ctx.SharedPlatforms
            .AsNoTracking()
            .Include(platform => platform.Admins)
            .Single(platform => platform.Id == id)
            .Admins;
        
        var users = new List<IdentityUser>();
        users.AddRange(facilitators);
        users.AddRange(admins);

        return users;
    }
    
    public IEnumerable<Project> ReadProjectsForPlatform(long platformId)
    {
        return _ctx.SharedPlatforms.Include(platform => platform.Projects)
            .Single(platform => platform.Id == platformId)
            .Projects;
    }

    public void CreateProjectToPlatform(Project project, long sharedPlatformId)
    {
        _ctx.SharedPlatforms.Single(platform => platform.Id == sharedPlatformId).Projects.Add(project);
    }

    public IEnumerable<SharedPlatform> ReadAllSharedPlatforms()
    {
        return _ctx.SharedPlatforms.AsNoTracking().ToList();
    }

    public SharedPlatform CreateSharedPlatform(SharedPlatform sharedPlatform)
    {
        return _ctx.SharedPlatforms.Add(sharedPlatform).Entity;
    }
}