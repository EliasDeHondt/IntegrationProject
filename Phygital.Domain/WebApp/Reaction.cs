/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.Accounts;

namespace Domain.WebApp;

public class Reaction
{
    public long Id { get; set; }
    [MaxLength(300)]
    public string Text { get; set; }
    public Idea Idea { get; set; }
    public WebAppUser WebAppUser { get; set; }

    public Reaction(string text, Idea idea, WebAppUser webAppUser, long id = default)
    {
        Id = id;
        Text = text;
        Idea = idea;
        WebAppUser = webAppUser;
    }
    
    public Reaction()
    {
        Id = default;
        Text = string.Empty;
        Idea = new Idea();
        WebAppUser = new WebAppUser();
    }
    
}