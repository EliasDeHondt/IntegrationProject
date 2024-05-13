using Data_Access_Layer;
using Domain.Platform;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Identity;

namespace Business_Layer;

public class SharedPlatformManager
{
    private readonly SharedPlatformRepository _repo;
    
    public SharedPlatformManager(SharedPlatformRepository repo)
    {
        _repo = repo;
    }

    public SharedPlatform GetSharedPlatformWithProjects(long id)
    {
        return _repo.ReadSharedPlatformIncludingProjects(id);
    }

    public SharedPlatform GetSharedPlatform(long id)
    {
        return _repo.ReadSharedPlatform(id);
    }
    
    public IEnumerable<IdentityUser> GetUsersForPlatform(long id)
    {
        return _repo.ReadUsersForPlatform(id);
    }

    public IEnumerable<Project> GetProjectsForPlatform(long platformId)
    {
        return _repo.ReadProjectsForPlatform(platformId);
    }

    public void AddProjectToPlatform(Project project, long sharedPlatformId)
    {
        _repo.CreateProjectToPlatform(project,sharedPlatformId);
    }

    public IEnumerable<SharedPlatform> GetAllSharedPlatforms()
    {
        return _repo.ReadAllSharedPlatforms();
    }

    public SharedPlatform AddSharedPlatform(string organisationName, string logo)
    {
        var platform = new SharedPlatform
        {
            OrganisationName = organisationName,
            Logo = logo
        };
        return _repo.CreateSharedPlatform(platform);
    }
}