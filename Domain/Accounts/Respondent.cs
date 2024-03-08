using Domain.WebApp;

namespace Domain.Accounts;

public class Respondent : User
{
    
    public override long Id { get; set; }
    public override String Name { get; set; }
    public override String Email { get; set; }
    public override string Password { get; set; }
    
    public List<Idea> Ideas { get; set; }
    public List<Reaction> Reactions { get; set; }
    public List<Like> Likes { get; set; }
    
    void CreatePost()
    {
    }
    
}