namespace Domain.ProjectLogics;

public class Project
{
    public long Id { get; set; }
    public MainTheme MainTheme { get; set; }

    public Project(long id,MainTheme mainTheme)
    {
        MainTheme = mainTheme;
        Id = id;
    }
    
}