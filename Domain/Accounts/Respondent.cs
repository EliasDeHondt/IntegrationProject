/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.WebApp;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public class Respondent : IdentityUser
{
    public Respondent(string email) 
    {
        Email = email;
    }

    public List<Idea> Ideas { get; set; }
    public List<Reaction> Reactions { get; set; }
    public List<Like> Likes { get; set; }
    public string Email { get; set; }

    // public Respondent(string userName, string email) : base(userName)
    // {
    //     Email = email;
    // }

    void CreatePost()
    {
    }
    
}