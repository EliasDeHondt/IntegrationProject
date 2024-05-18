using Data_Access_Layer.DbContext;
using Domain.Accounts;
using Domain.WebApp;

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
        var admin = _ctx.Users.Single(u => u.Email == email) as SpAdmin;
        _ctx.Entry(admin!).Reference(a => a.SharedPlatform).Load();
        return admin!;
    }
    
    public long ReadSharedPlatformId(string email)
    {
        var user = _ctx.Users.Single(u => u.Email == email) as SpAdmin;
        _ctx.Entry(user!).Reference(u => u.SharedPlatform).Load();
        return user!.SharedPlatform.Id;
    }

    public long ReadRandomFeedIdFromUser(string email)
    {
        var user = _ctx.Users.Single(u => u.Email == email) as WebAppUser;
        _ctx.Entry(user!).Collection(u => u.FeedIds).Load();
        var rand = new Random();
        return user!.FeedIds.ElementAt(rand.Next(0, user.FeedIds.Count - 1)).Value;
    } 
    
}