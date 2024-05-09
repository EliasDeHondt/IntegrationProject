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
    public long? SharedPlatformId { get; set; }
    [Required]
    public ICollection<ProjectOrganizer> ManagedProjects { get; set; }


    public Facilitator(long sharedPlatformId, ICollection<ProjectOrganizer> managedProjects)
    {
        SharedPlatformId = sharedPlatformId;
        ManagedProjects = managedProjects;
    }
    
    public Facilitator(long sharedPlatformId)
    {
        SharedPlatformId = sharedPlatformId;
        ManagedProjects = new List<ProjectOrganizer>();
    }

    public Facilitator()
    {
        SharedPlatformId = default;
        ManagedProjects = new List<ProjectOrganizer>();
    }
    
}