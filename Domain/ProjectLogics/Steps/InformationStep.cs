using System.ComponentModel.DataAnnotations;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class InformationStep : IStep
{
    [Required]
    [Range(0, int.MaxValue)]
    public int StepNumber { get; set; }
    [Key]
    public long Id { get; set; }
    
    [Required]
    public IInformation Information { get; set; }
    public IQuestion? Question { get; set; }
    [Required]
    public Note Note { get; set; } = new();
    [Required]
    public Flow Flow { get; set; }
    
    
    
    public InformationStep(int stepNumber, IInformation information, Flow flow, long id = 0)
    {
        Id = id;
        StepNumber = stepNumber;
        Information = information;
        Flow = flow;
    }

    public InformationStep()
    {
        Id = default;
        StepNumber = default;
        Information = new Text();
        Flow = new Flow();
    }
    
}