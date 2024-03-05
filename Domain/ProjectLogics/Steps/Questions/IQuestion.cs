namespace Domain.ProjectLogics.Steps.Questions;

public interface IQuestion<out T>
{

    string Question { get; set; }
    T Answer();
    IEnumerable<string> GetChoices();

}