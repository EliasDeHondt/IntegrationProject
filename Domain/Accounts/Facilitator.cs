/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.ProjectLogics;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public class Facilitator : IdentityUser
{
    public long SharedPlatformId { get; set; }
    public IEnumerable<Project> Projects { get; set; }
}