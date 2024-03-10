using Domain.WebApp;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public class Respondent : IdentityUser
{
    
    public List<Idea> Ideas { get; set; }
    public List<Reaction> Reactions { get; set; }
    public List<Like> Likes { get; set; }
    
    void CreatePost()
    {
    }
    
}