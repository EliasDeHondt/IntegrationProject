using Domain.WebApp;

namespace Domain.Accounts;

public class Respondent : IUser
{
    
    public long Id { get; set; }
    public String Name { get; set; }
    public String Email { get; set; }
    public string Password { get; set; }
    
    public List<Idea> Ideas { get; set; }
    public List<Reaction> Reactions { get; set; }
    public List<Like> Likes { get; set; }
    
    void CreatePost()
    {
    }
    
}