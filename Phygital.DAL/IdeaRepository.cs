using Data_Access_Layer.DbContext;
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
}