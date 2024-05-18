namespace MVC.Models.feedModels;

public class IdeaModel
{
    public long Id { get; set; }
    public IEnumerable<LikeModel> likes { get; set; }
    public string Text { get; set; }
    public AuthorModel author { get; set; }
}