namespace Domain.WebApp;

public class Idea
{
    public String Text { get; set; }
    public List<Reaction> Reactions { get; set; }
    public List<Like> Likes { get; set;  }
}