/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.Accounts;
using Domain.ProjectLogics;

namespace Domain.FacilitatorFunctionality;

public class ProjectOrganizer
{
    public Project Project { get; set; }
    public Note Note { get; set; }
    public Facilitator Facilitator { get; set; }
}