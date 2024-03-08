namespace Domain.ProjectLogics.Steps.Questions;

public class SingleChoiceQuestion : IQuestion<string>
{
    public IEnumerable<string> Choices { get; set; }
    public string Question { get; set; }
    public IStep Step { get; set; }

    public SingleChoiceQuestion(string question, IEnumerable<string> choices)
    {
        Question = question;
        Choices = choices;
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
        return Choices;
    }
}