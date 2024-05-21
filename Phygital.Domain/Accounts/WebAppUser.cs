/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.WebApp;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public class WebAppUser : IdentityUser
{
   public ICollection<LongValue> FeedIds { get; set; }
   public ICollection<Idea> Ideas { get; set; }
   public ICollection<Like> Likes { get; set; }
   public ICollection<Reaction> Reactions { get; set; }

   public WebAppUser()
   {
       FeedIds = new List<LongValue>();
       Ideas = new List<Idea>();
       Likes = new List<Like>();
       Reactions = new List<Reaction>();
   }
   
}

public class LongValue
{
    public long Id { get; set; }
    public long Value { get; set; }
}