namespace Domain.ProjectLogics.Steps.Questions;

public class Answer
{
    public long Id { get; set; }
    public IQuestion<object> Question { get; set; }
}