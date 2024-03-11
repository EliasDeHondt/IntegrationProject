/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
namespace Domain.ProjectLogics;

public abstract class ThemeBase
{
    [Key]
    public long Id { get; set; }
    [MaxLength(50)]
    public string Subject { get; set; }
    [Required]
    public ICollection<Flow> Flows { get; set; }

    protected ThemeBase(string subject, ICollection<Flow> flows, long id = 0)
    {
        Subject = subject;
        Flows = flows;
        Id = id;
    }

    protected ThemeBase (string subject, long id = 0)
    {
        Subject = subject;
        Id = id;
        Flows = new List<Flow>();
    }
    
    protected ThemeBase()
    {
        Subject = string.Empty;
        Flows = new List<Flow>();
        Id = default;
    }
}