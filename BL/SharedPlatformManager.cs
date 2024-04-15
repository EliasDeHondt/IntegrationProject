using Data_Access_Layer;
using Domain.Platform;

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
    
}