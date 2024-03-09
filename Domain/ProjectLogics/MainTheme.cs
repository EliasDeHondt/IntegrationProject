using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics;

public class MainTheme : ITheme
{
    [Key]
    public long Id { get; set; }
    [MaxLength(50)]
    public string Subject { get; set; }
    [Required]
    public ICollection<Flow> Flows { get; set; }
    [Required]
    public IEnumerable<SubTheme> Themes { get; set; }
    
    public MainTheme(string subject, ICollection<Flow> flows, IEnumerable<SubTheme> themes, long id = 0)
    {
        Id = id;
        Subject = subject;
        Flows = flows;
        Themes = themes;
    }
    
    public MainTheme(string subject, long id = 0)
    {
        Id = id;
        Subject = subject;
        Flows = new List<Flow>();
        Themes = new List<SubTheme>();
    }

    public MainTheme()
    {
        Id = default;
        Subject = string.Empty;
        Flows = new List<Flow>();
        Themes = new List<SubTheme>();
    }
    
}