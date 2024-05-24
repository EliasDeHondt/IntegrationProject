/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.Accounts;
using FileHelpers;

namespace Domain.ProjectLogics;
//[FixedLengthRecord] 
public class Participation
{
    //[FieldFixedLength(length:100)]
    public long Id { get; set; }
    //[FieldFixedLength(length:100)]
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