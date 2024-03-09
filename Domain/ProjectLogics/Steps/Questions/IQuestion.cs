namespace Domain.ProjectLogics.Steps.Questions;

public interface IQuestion
{

    long Id { get; set; }
    ICollection<Answer> Answers { get; set; }
    string Question { get; set; }
    object Answer();
    IEnumerable<string> GetChoices();

}