/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

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