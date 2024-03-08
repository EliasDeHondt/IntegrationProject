namespace Domain.ProjectLogics;

public class Theme
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public IEnumerable<Theme> Themes { get; set; }
    
}