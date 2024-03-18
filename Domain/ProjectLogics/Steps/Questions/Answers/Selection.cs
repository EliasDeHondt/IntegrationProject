using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions.Answers;

public class Selection
{
    [Required]
    public Choice Choice { get; set; }
    [Required]
    public ChoiceAnswer ChoiceAnswer { get; set; }
}