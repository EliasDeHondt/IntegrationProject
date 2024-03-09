using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions;

public class OpenQuestion : IQuestion
{
    [Required]
    [MaxLength(600)]
    public string TextField { get; set; } = string.Empty;
    [Required]
    [MaxLength(150)]
    public string Question { get; set; }
    [Key]
    public long Id { get; set; }
    [Required]
    public ICollection<Answer> Answers { get; set; }

    public OpenQuestion(string question, ICollection<Answer> answers, long id = 0) : this(question, id)
    {
        Answers = answers;
    }
    
    public OpenQuestion(string question, long id = 0)
    {
        Id = id;
        Question = question;
        Answers = new List<Answer>();
    }

    public OpenQuestion()
    {
        Id = default;
        Question = string.Empty;
        TextField = string.Empty;
        Answers = new List<Answer>();
    }
    
    private string SelectOne()
    {
        throw new NotImplementedException();
    }

    public object Answer()
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<string> GetChoices()
    {
        return new []{TextField};
    }
    
}