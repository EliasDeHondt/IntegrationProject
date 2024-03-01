namespace Domain.ProjectLogics.Steps.Questions;

public class MultipleChoiceQuestion : IQuestion<List<string>>
{
    
    public IEnumerable<string> Choices { get; set; }
    public string Question { get; set; }
    
    private List<string> SelectMultiple()
    {
        throw new NotImplementedException();
    }
    
    public List<string> Answer()
    {
        throw new NotImplementedException();
    }
}