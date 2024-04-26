using Data_Access_Layer;
using Domain.Platform;
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
    
}