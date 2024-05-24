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
    public long Id { get; set; }
    [MaxLength(600)]
    public string Subject { get; set; }
    public ICollection<Flow> Flows { get; set; }

    protected ThemeBase(string subject, ICollection<Flow> flows, long id = 0)
    {
        Subject = subject;
        Flows = flows;
        Id = id == 0 ? default : id;
    }

    protected ThemeBase (string subject, long id = 0)
    {
        Subject = subject;
        Id = id == 0 ? default : id;
        Flows = new List<Flow>();
    }
    
    protected ThemeBase()
    {
        Subject = string.Empty;
        Flows = new List<Flow>();
        Id = default;
    }
}