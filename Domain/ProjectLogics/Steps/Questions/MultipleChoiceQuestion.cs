namespace Domain.ProjectLogics.Steps.Questions;

public class MultipleChoiceQuestion : IQuestion<IEnumerable<string>>
{
    
    public IEnumerable<string> Choices { get; set; }
    public long Id { get; set; }
    public IStep Step { get; set; }
    public IEnumerable<Answer> Answers { get; set; }
    public string Question { get; set; }

    public MultipleChoiceQuestion(string question, IEnumerable<string> choices)
    {
        Question = question;
        Choices = choices;
    }
    
    private IEnumerable<string> SelectMultiple()
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<string> Answer()
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<string> GetChoices()
    {
        return Choices;
    }
    
}