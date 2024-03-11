using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics;

public class MainTheme : ThemeBase
{
    [Required]
    public IEnumerable<SubTheme> Themes { get; set; }
    
    public MainTheme(string subject, ICollection<Flow> flows, IEnumerable<SubTheme> themes, long id = 0) : base(subject, flows, id)
    {
        Themes = themes;
    }
    
    public MainTheme(string subject, long id = 0) : base(subject, id)
    {
        Themes = new List<SubTheme>();
    }

    public MainTheme()
    {
        Themes = new List<SubTheme>();
    }
    
}