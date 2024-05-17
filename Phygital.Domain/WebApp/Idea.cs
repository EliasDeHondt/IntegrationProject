/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.Accounts;

namespace Domain.WebApp;

public class Idea
{
    public long Id { get; set; }
    [MaxLength(600)]
    public string Text { get; set; }
    public ICollection<Reaction> Reactions { get; set; }
    public ICollection<Like> Likes { get; set;  }
    public WebAppUser Author { get; set; }
    public Feed Feed { get; set; }

    public Idea(string text, WebAppUser author, Feed feed, long id = default)
    {
        Id = id;
        Text = text;
        Reactions = new List<Reaction>();
        Likes = new List<Like>();
        Feed = feed;
        Author = author;
        
    }
    
    public Idea()
    {
        Id = default;
        Text = string.Empty;
        Reactions = new List<Reaction>();
        Likes = new List<Like>();
        Author = new WebAppUser();
        Feed = new Feed();
    }
    
}