/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.Platform;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public abstract class User : IdentityUser
{
    // public abstract long UserId { get; set; }

    public void Login()
    {
    }
}