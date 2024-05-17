namespace Domain.WebApp;

public class Feed
{
    public long Id { get; set; }
    public ICollection<Idea> Ideas { get; set; }

    public Feed()
    {
        Ideas = new List<Idea>();
    }
    
    
}