namespace Domain.ProjectLogics;

public class Project
{
    public long Id { get; set; }
    public MainTheme MainTheme { get; set; }

    public Project(MainTheme mainTheme, long id)
    {
        MainTheme = mainTheme;
        Id = id;
    }
    
}