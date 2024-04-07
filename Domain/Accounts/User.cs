/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public abstract class User : IdentityUser
{
    public abstract long UserId { get; set; }
    public abstract string Name { get; set; }
    public abstract string Email { get; set; }
    public abstract string Password { get; set; }

    public void Login()
    {
    }
}