using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics;

public class SubTheme : ITheme
{
    [Key]
    public long Id { get; set; }
    [MaxLength(50)]
    public string Subject { get; set; }
    [Required]
    public ICollection<Flow> Flows { get; set; }
    [Required]
    public MainTheme MainTheme { get; set; }

    public SubTheme(string subject, ICollection<Flow> flows, MainTheme mainTheme, long id = 0)
    {
        Id = id;
        Subject = subject;
        Flows = flows;
        MainTheme = mainTheme;
    }

    public SubTheme()
    {
        Id = default;
        Subject = string.Empty;
        Flows = new List<Flow>();
        MainTheme = new MainTheme();
    }
}