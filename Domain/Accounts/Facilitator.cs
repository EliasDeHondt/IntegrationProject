/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.Platform;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public class Facilitator : IdentityUser
{
    public string Name { get; set; }
    public string Password { get; set; }
    public SharedPlatform SharedPlatform { get; set; }
}