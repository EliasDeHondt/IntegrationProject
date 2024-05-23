using Data_Access_Layer.DbContext;
using Domain.WebApp;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer;

public class ReactionRepository
{
    private readonly CodeForgeDbContext _ctx;

    public ReactionRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }

    public Reaction CreateReaction(Reaction reaction)
    {
        _ctx.Reactions.Add(reaction);
        return reaction;
    }

    public IEnumerable<Reaction> ReadReactionsForIdeaIncludingUser(long id)
    {
        return _ctx.Reactions.Include(reaction => reaction.WebAppUser)
            .Where(reaction => reaction.Idea.Id == id)
            .ToList();
    }
}