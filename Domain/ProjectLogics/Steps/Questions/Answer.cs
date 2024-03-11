using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions;

public class Answer
{
    [Key]
    public long Id { get; set; }
    [Required]
    public QuestionBase QuestionBase { get; set; }
    
    public Answer(QuestionBase questionBase, long id = 0)
    {
        Id = id;
        QuestionBase = questionBase;
    }
    
    public Answer()
    {
        Id = default;
        QuestionBase = new SingleChoiceQuestion();
    }
    
}