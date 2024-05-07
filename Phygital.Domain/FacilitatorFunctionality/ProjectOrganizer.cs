/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.Accounts;
using Domain.ProjectLogics;

namespace Domain.FacilitatorFunctionality;

public class ProjectOrganizer
{
    [Required]
    public Project Project { get; set; }
    [Required]
    public Facilitator Facilitator { get; set; }
    
    public ProjectOrganizer(Project project, Facilitator facilitator)
    {
        Project = project;
        Facilitator = facilitator;
    }

    public ProjectOrganizer()
    {
        Project = new Project();
        Facilitator = new Facilitator();
    }
    
}