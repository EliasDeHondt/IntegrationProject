using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions;

public class Answer
{
    [Key]
    public long Id { get; set; }
    [Required]
    public IQuestion Question { get; set; }
    
    public Answer(IQuestion question, long id = 0)
    {
        Id = id;
        Question = question;
    }
    
    public Answer()
    {
        Id = default;
        Question = new SingleChoiceQuestion();
    }
    
}