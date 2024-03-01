namespace Domain.ProjectLogics.Steps.Questions;

public class RangeQuestion : IQuestion<int>
{
    
    public IEnumerable<string> Choices { get; set; }
    public string Question { get; set; }
    
    private int SelectOne()
    {
        throw new NotImplementedException();
    }

    public int Answer()
    {
        throw new NotImplementedException();
    }
}