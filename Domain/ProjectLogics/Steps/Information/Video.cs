namespace Domain.ProjectLogics.Steps.Information;

public class Video : IInformation
{
    
    public string FilePath { get; set; }
    public IStep Step { get; set; }

    public Video(string filePath)
    {
        FilePath = filePath;
    }
    

    public string GetInformation()
    {
        return FilePath;
    }
}