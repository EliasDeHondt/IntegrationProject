namespace Domain.ProjectLogics.Steps.Questions;

public class OpenQuestion : IQuestion<string>
{

    public string TextField { get; set; } = string.Empty;
    public string Question { get; set; }
    public IStep Step { get; set; }

    public OpenQuestion(string question)
    {
        Question = question;
    }

    private string SelectOne()
    {
        throw new NotImplementedException();
    }

    public string Answer()
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<string> GetChoices()
    {
        return new []{TextField};
    }
    
}