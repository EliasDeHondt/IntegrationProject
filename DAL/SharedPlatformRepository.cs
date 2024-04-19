using Data_Access_Layer.DbContext;
using Domain.Platform;
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
            .Include(platform => platform.Projects)
            .First(platform => platform.Id == id);
    }
    
}