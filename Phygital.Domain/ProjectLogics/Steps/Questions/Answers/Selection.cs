using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions.Answers;

public class Selection
{
    public Choice Choice { get; set; }
    public ChoiceAnswer ChoiceAnswer { get; set; }
}