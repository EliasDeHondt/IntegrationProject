namespace Domain.WebApp;

public class Respondent
{
    public long RespondentId { get; set; }
    public String Name { get; set; }
    public String Email { get; set; }

    public List<Idea> Ideas { get; set; }
    public List<Reaction> Reactions { get; set; }
    public List<Like> Likes { get; set; }
    
    void CreatePost()
    {
    }
}