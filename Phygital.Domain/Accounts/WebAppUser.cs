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
   public ICollection<Feed> Feeds { get; set; }
   public ICollection<Idea> Ideas { get; set; }
   public ICollection<Like> Likes { get; set; }
   public ICollection<Reaction> Reactions { get; set; }
}