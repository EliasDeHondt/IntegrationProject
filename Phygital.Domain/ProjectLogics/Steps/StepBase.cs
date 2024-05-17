/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.FacilitatorFunctionality;

namespace Domain.ProjectLogics.Steps;

public abstract class StepBase
{
    [Range(0, int.MaxValue)]
    public int StepNumber { get; set; }
    public long Id { get; set; }
    public ICollection<Note> Notes { get; set; }
    public Flow Flow { get; set; }
    
    protected StepBase(int stepNumber, Flow flow, long id = 0)
    {
        StepNumber = stepNumber;
        Flow = flow;
        Id = id;
        Notes = new List<Note>();
    }

    protected StepBase()
    {
        StepNumber = default;
        Flow = new Flow();
        Id = default; 
        Notes = new List<Note>();
    }
}