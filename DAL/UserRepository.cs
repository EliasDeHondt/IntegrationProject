using Data_Access_Layer.DbContext;
using Domain.Accounts;
using Domain.Platform;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer;

public class UserRepository
{
    
    private readonly CodeForgeDbContext _ctx;
    
    public UserRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }

    public SpAdmin ReadPlatformAdminIncludingSharedPlatform(string email)
    {
        var admin = _ctx.Users.First(u => u.Email == email) as SpAdmin;
        _ctx.Entry(admin!).Reference(admin => admin.SharedPlatform).Load();
        return admin;
    }
    
    public long ReadSharedPlatformId(string email)
    {
        var user = (_ctx.Users.First(u => u.Email == email) as SpAdmin);
        _ctx.Entry(user!).Reference(user => user.SharedPlatform).Load();
        return user.SharedPlatform.Id;
    }
    
}