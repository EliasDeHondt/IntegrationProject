/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.Accounts;

namespace Domain.ProjectLogics;

public class Participation
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public List<Respondent> Respondents { get; set; } // enkel degene die een email invulden
    
    public Participation(Flow flow, long id = 0)
    {
        Id = id;
        Flow = flow;
        Respondents = new List<Respondent>();
    }

    public Participation()
    {
        Id = default;
        Flow = new Flow();
        Respondents = new List<Respondent>();
    }
}