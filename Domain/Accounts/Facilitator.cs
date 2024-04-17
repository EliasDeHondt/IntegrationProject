/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public class Facilitator : IdentityUser
{
    [Required]
    public long SharedPlatformId { get; set; }
    [Required]
    public ICollection<Project> Projects { get; set; }
}