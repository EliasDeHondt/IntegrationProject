using Data_Access_Layer.DbContext;
using Domain.Platform;
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
            .Include(platform => platform.Projects)
            .First(platform => platform.Id == id);
    }

    public SharedPlatform ReadSharedPlatform(long id)
    {
        return _ctx.SharedPlatforms.Find(id);
    }

    public IEnumerable<IdentityUser> ReadUsersForPlatform(long id)
    {
        var facilitators = _ctx.SharedPlatforms
            .Include(platform => platform.Faciliators)
            .First(platform => platform.Id == id)
            .Faciliators;

        var admins = _ctx.SharedPlatforms
            .Include(platform => platform.Admins)
            .First(platform => platform.Id == id)
            .Admins;
        
        var users = new List<IdentityUser>();
        users.AddRange(facilitators);
        users.AddRange(admins);

        return users;
    }
}