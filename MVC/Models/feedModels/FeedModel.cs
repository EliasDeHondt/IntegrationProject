namespace MVC.Models.feedModels;

public class FeedModel
{
    public IEnumerable<IdeaModel> Ideas { get; set; }
    public string Title { get; set; }
    public long Id { get; set; }
}