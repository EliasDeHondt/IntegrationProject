using System.ComponentModel.DataAnnotations;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class QuestionStep : IStep
{
    [Required]
    [Range(0, int.MaxValue)]
    public int StepNumber { get; set; }
    [Key]
    public long Id { get; set; }
    public IInformation? Information { get; set; } 
    [Required]
    public Note Note { get; set; } = new();
    [Required]
    public Flow Flow { get; set; }
    [Required]
    public IQuestion Question { get; set; }
    
    public QuestionStep(int stepNumber, IQuestion question, Flow flow , long id = 0)
    {
        Id = id;
        Flow = flow;
        StepNumber = stepNumber;
        Question = question;
    }

    public QuestionStep()
    {
        Id = default;
        Flow = new Flow();
        StepNumber = default;
        Question = new SingleChoiceQuestion();
    }
    
    
}