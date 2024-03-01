namespace Domain.ProjectLogics.Steps.Questions;

public class SingleChoiceQuestion : IQuestion<string>
{
    public IEnumerable<string> Choices { get; set; }
    public string Question { get; set; }

    private string SelectOne()
    {
        throw new NotImplementedException();
    }

    public string Answer()
    {
        throw new NotImplementedException();
    }
}