using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions;

public class RangeQuestion : IQuestion
{
    [Required]
    public ICollection<Choice> Choices { get; set; }
    [Required]
    [MaxLength(150)]
    public string Question { get; set; }
    [Key]
    public long Id { get; set; }
    [Required]
    public ICollection<Answer> Answers { get; set; }
    
    public RangeQuestion(string question, ICollection<Choice> choices, ICollection<Answer> answers, long id = 0) : this(question, choices, id)
    {
        Answers = answers;
    }
    
    public RangeQuestion(string question, ICollection<Choice> choices, long id = 0)
    {
        Id = id;
        Question = question;
        Choices = choices;
        Answers = new List<Answer>();
    }

    public RangeQuestion()
    {
        Id = default;
        Question = string.Empty;
        Choices = new List<Choice>();
        Answers = new List<Answer>();
    }
    private int SelectOne()
    {
        throw new NotImplementedException();
    }

    public object Answer()
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<string> GetChoices()
    {
        throw new NotImplementedException();
    }
    
}