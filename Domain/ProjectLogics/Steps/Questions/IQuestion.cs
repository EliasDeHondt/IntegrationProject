namespace Domain.ProjectLogics.Steps.Questions;

public interface IQuestion<out T>
{

    long Id { get; set; }
    IStep Step { get; set; }
    IEnumerable<Answer> Answers { get; set; }
    string Question { get; set; }
    T Answer();
    IEnumerable<string> GetChoices();

}