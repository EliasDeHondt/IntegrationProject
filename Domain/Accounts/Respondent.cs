/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.ProjectLogics;
using Domain.WebApp;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public class Respondent 
{
    public Respondent(string email,Participation participation) 
    {
        Email = email;
        Participation = participation;
        // Ideas = new List<Idea>();
        // Reactions = new List<Reaction>();
        // Likes = new List<Like>();
    }
    public Respondent()
    {
        Email = "none";
        Participation = new Participation();
    }

    public long RespondentId { get; set; }
    //todo naar user
    // public List<Idea> Ideas { get; set; }
    // public List<Reaction> Reactions { get; set; }
    // public List<Like> Likes { get; set; }
    public string Email { get; set; }
    public Participation Participation { get; set; }

    // public Respondent(string userName, string email) : base(userName)
    // {
    //     Email = email;
    // }

    void CreatePost()
    {
    }
    
}