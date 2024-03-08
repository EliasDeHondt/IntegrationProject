namespace Domain.ProjectLogics;

public class MainTheme : ITheme
{
    public long Id { get; set; }
    public string Subject { get; set; }
    public IEnumerable<Flow> Flows { get; set; }
    public Project Project { get; set; }
    public IEnumerable<SubTheme> Themes { get; set; }

    public MainTheme(long id, string subject, IEnumerable<Flow> flows, Project project, IEnumerable<SubTheme> themes)
    {
        Id = id;
        Subject = subject;
        Flows = flows;
        Project = project;
        Themes = themes;
    }
    
    public MainTheme(long id, string subject, Project project)
    {
        Id = id;
        Subject = subject;
        Flows = new List<Flow>();
        Project = project;
        Themes = new List<SubTheme>();
    }
}