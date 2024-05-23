using Data_Access_Layer;
using Domain.Accounts;
using Domain.WebApp;

namespace Business_Layer;

public class ReactionManager
{
    
    private readonly ReactionRepository _repo;
    
    public ReactionManager(ReactionRepository repo)
    {
        _repo = repo;
    }
    
    public Reaction AddReaction(Idea idea, WebAppUser user, string text)
    {
        Reaction reaction = new Reaction(text, idea, user);
        
        return _repo.CreateReaction(reaction);
    }

    public IEnumerable<Reaction> GetReactionsForIdea(long id)
    {
        return _repo.ReadReactionsForIdeaIncludingUser(id);
    }
}