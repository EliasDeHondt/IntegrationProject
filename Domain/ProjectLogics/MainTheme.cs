namespace Domain.ProjectLogics;

public class MainTheme : ITheme
{
    public long Id { get; set; }
    public IEnumerable<Flow> Flows { get; set; }
    public Project Project { get; set; }
    public IEnumerable<SubTheme> Themes { get; set; }
}