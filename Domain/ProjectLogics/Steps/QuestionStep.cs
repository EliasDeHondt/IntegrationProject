using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class QuestionStep : StepBase
{
    [Required]
    public QuestionBase QuestionBase { get; set; }
    
    public QuestionStep(int stepNumber, QuestionBase questionBase, Flow flow , long id = 0) : base(stepNumber, flow, id)
    {
        QuestionBase = questionBase;
    }

    public QuestionStep()
    {
        QuestionBase = new SingleChoiceQuestion();
    }
    
    
}