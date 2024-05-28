using Data_Access_Layer.DbContext;
using Domain.Accounts;
using Domain.WebApp;
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
 
    public IEnumerable<Feed> ReadFeedsFromUserIncludingProject(string email)
    {
        var user = _ctx.Users.Single(u => u.Email == email) as WebAppUser;
        _ctx.Entry(user!).Collection(u => u.FeedIds).Load();
        var feedIds = user!.FeedIds.Select(fid => fid.Value);
        return _ctx.Feeds.Include(feed => feed.Project)
            .Where(feed => feedIds.Contains(feed.Id));
    }

    public void AddFeedToUser(LongValue feed, string email)
    {
        var user = _ctx.Users.Single(u => u.Email == email) as WebAppUser;
        user!.FeedIds.Add(feed);
    }

    public void DeleteFeedFromUser(LongValue feedValue, string email)
    {
        var user = _ctx.Users.Single(u => u.Email == email) as WebAppUser;
        _ctx.Entry(user!).Collection(u => u.FeedIds).Load();
        var value = user!.FeedIds.Single(l => l.Value == feedValue.Value);
        user.FeedIds.Remove(value);
    }
}