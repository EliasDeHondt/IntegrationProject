/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.Platform;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public class SpAdmin: User
{
 public override string Name { get; set; }
 public override string Email { get; set; }
 public override string Password { get; set; }
}