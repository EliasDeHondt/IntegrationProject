using Data_Access_Layer;
using Domain.WebApp;

namespace Business_Layer;

public class FeedManager
{

    private readonly FeedRepository _repo;

    public FeedManager(FeedRepository repo)
    {
        _repo = repo;
    }

    public Feed GetFeedFromIdWithIdeas(long id)
    {
        return _repo.ReadFeedFromIdIncludingIdeas(id);
    }

    public Feed GetFeed(long id)
    {
        return _repo.ReadFeed(id);
    }
    
}