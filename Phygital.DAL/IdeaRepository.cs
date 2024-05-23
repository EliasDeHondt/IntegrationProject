using Data_Access_Layer.DbContext;
using Domain.Accounts;
using Domain.WebApp;

namespace Data_Access_Layer;

public class IdeaRepository
{
    private readonly CodeForgeDbContext _ctx;
    
    public IdeaRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public void CreateIdea(Idea idea)
    {
        _ctx.Ideas.Add(idea);
    }

    public Idea ReadIdea(long ideaId)
    {
        return _ctx.Ideas.Find(ideaId)!;
    }

    public Like CreateLike(Like like)
    {
        _ctx.Likes.Add(like);
        return like;
    }

    public void DeleteLike(Idea idea, WebAppUser user)
    {
        var like = _ctx.Likes.Single(like => like.Idea == idea && like.WebAppUser == user);
        _ctx.Likes.Remove(like);
    }
}