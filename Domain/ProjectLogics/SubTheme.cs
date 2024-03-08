namespace Domain.ProjectLogics;

public class SubTheme : ITheme
{
    public long Id { get; set; }
    public string Subject { get; set; }
    public IEnumerable<Flow> Flows { get; set; }
    public MainTheme MainTheme { get; set; }

    public SubTheme(long id, string subject, IEnumerable<Flow> flows, MainTheme mainTheme)
    {
        Id = id;
        Subject = subject;
        Flows = flows;
        MainTheme = mainTheme;
    }
}