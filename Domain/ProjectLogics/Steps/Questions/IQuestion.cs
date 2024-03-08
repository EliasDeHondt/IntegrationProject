namespace Domain.ProjectLogics.Steps.Questions;

public interface IQuestion<out T>
{

    IStep Step { get; set; }
    string Question { get; set; }
    T Answer();
    IEnumerable<string> GetChoices();

}