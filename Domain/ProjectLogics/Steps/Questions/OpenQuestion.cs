namespace Domain.ProjectLogics.Steps.Questions;

public class OpenQuestion : IQuestion<string>
{
    
    public string TextField { get; set; }
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