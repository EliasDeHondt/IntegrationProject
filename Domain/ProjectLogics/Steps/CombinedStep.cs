using System.ComponentModel.DataAnnotations;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class CombinedStep : IStep
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
    [Required]
    public IInformation Information { get; set; }
    [Required]
    public IQuestion Question { get; set; }
    
    public CombinedStep(int stepNumber, IInformation information, IQuestion question, Flow flow, long id = 0)
    {
        Id = id;
        Flow = flow;
        StepNumber = stepNumber;
        Information = information;
        Question = question;
    }

    public CombinedStep()
    {
        Id = default;
        Flow = new Flow();
        StepNumber = default;
        Information = new Text();
        Question = new SingleChoiceQuestion();
    }
    
}