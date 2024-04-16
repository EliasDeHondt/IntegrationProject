/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.Platform;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public class SpAdmin: IdentityUser
{
    public SharedPlatform SharedPlatform { get; set; }
}