namespace Domain.ProjectLogics.Steps.Information;

public class Image : IInformation
{
    
    public string FilePath { get; set; }

    public Image(string path)
    {
        FilePath = path;
    }

    public string GetInformation()
    {
        return FilePath;
    }
}