namespace Domain.ProjectLogics;

public class MainTheme
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Project Project { get; set; }
    public IEnumerable<SubTheme> Themes { get; set; }
}