using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions;

public class OpenQuestion : QuestionBase
{
    [Required]
    [MaxLength(600)]
    public string TextField { get; set; } = string.Empty;

    public OpenQuestion(string question, ICollection<Answer> answers, long id = 0) : base(answers, question, id)
    {
    }
    
    public OpenQuestion(string question, long id = 0) : base(question, id)
    {
    }

    public OpenQuestion()
    {
    }
    
    private string SelectOne()
    {
        throw new NotImplementedException();
    }

    public override object Answer()
    {
        throw new NotImplementedException();
    }
    
    public override IEnumerable<string> GetChoices()
    {
        return new []{TextField};
    }
    
}