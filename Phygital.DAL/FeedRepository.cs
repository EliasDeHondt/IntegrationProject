using Data_Access_Layer.DbContext;
using Domain.WebApp;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer;

public class FeedRepository
{
    public CodeForgeDbContext _ctx { get; set; }

    public FeedRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }

    public Feed ReadFeedFromIdIncludingIdeas(long id)
    {
        return _ctx.Feeds.Include(feed => feed.Ideas)
            .ThenInclude(idea => idea.Author)
            .Include(feed => feed.Ideas)
            .ThenInclude(idea => idea.Likes)
            .ThenInclude(like => like.WebAppUser)
            .Include(feed => feed.Ideas)
            .ThenInclude(idea => idea.Image)
            .Include(feed => feed.Project)
            .Single(feed => feed.Id == id);
    }

    public Feed ReadFeed(long id)
    {
        return _ctx.Feeds.Single(feed => feed.Id == id);
    }
}