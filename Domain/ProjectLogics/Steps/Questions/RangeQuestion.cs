namespace Domain.ProjectLogics.Steps.Questions;

public class RangeQuestion : IQuestion<int>
{
    
    public IEnumerable<string> Choices { get; set; }
    public string Question { get; set; }
    public long Id { get; set; }
    public IStep Step { get; set; }
    public IEnumerable<Answer> Answers { get; set; }
    
    public RangeQuestion(string question, IEnumerable<string> choices)
    {
        Question = question;
        Choices = choices;
    }
    
    private int SelectOne()
    {
        throw new NotImplementedException();
    }

    public int Answer()
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<string> GetChoices()
    {
        return Choices;
    }
    
}