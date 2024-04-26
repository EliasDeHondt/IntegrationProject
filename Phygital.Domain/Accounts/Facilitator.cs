/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Identity;

namespace Domain.Accounts;

public class Facilitator : IdentityUser
{
    public long SharedPlatformId { get; set; }
    public ICollection<ProjectOrganizer> ManagedProjects { get; set; }
}