/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.Accounts;

namespace Domain.ProjectLogics;

public class Participation
{
    [Key]
    public long Id { get; set; }
    [Required]
    public Flow Flow { get; set; }
    public List<Respondent> Respondents { get; set; } // enkel degene die een email invulden
    
    public Participation(Flow flow, long id = 0)
    {
        Id = id;
        Flow = flow;
    }

    public Participation()
    {
        Id = default;
        Flow = new Flow();
    }
}