/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public abstract class StepBase
{
    [Required]
    [Range(0, int.MaxValue)]
    public int StepNumber { get; set; }
    [Key]
    public long Id { get; set; }
    [Required]
    public Note Note { get; set; } = new();
    [Required]
    public Flow Flow { get; set; }
    
    protected StepBase(int stepNumber, Flow flow, long id = 0)
    {
        StepNumber = stepNumber;
        Flow = flow;
        Id = id;
    }

    protected StepBase()
    {
        StepNumber = default;
        Flow = new Flow();
        Id = default;
    }
}