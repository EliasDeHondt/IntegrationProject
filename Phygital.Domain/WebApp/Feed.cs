using Domain.ProjectLogics;

namespace Domain.WebApp;

public class Feed
{
    public long Id { get; set; }
    public ICollection<Idea> Ideas { get; set; }
    public Project Project { get; set; }

    public Feed(Project project)
    {
        Ideas = new List<Idea>();
        Project = project;
        Id = default;
    }
    
    public Feed()
    {
        Project = new Project();
        Ideas = new List<Idea>();
        Id = default;
    }
    
    
}