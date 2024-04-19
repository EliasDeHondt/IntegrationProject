/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer.DbContext;
using Domain.Accounts;
using Domain.ProjectLogics;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer;

public class FlowRepository
{
    private readonly CodeForgeDbContext _context;

    public FlowRepository(CodeForgeDbContext context)
    {
        _context = context;
    }

    public Flow ReadFlowById(long id)
    {
        return _context.Flows.Find(id);
    }
    
    public Flow ReadFlowByIdIncludingTheme(long id)
    {
        return _context.Flows
            .Include(f => f.Theme)
            .Include(f => f.Steps)
            .FirstOrDefault(f => f.Id == id);
    }
    
    public void AddParticipationByFlow(long flowId,string email)
    {
        var participations = _context.Participations;//ReadFlowById(flowId).Participations;
       // var name = email.Substring(0, email.IndexOf('@')); //aa.ww@email.com --> aa.ww
        //Respondent respondent = ;

        Participation participation = new Participation(ReadFlowById(flowId));
        participation.Respondents.Add(new Respondent(email,participation)); //respondent
        participations.Add(participation);
        _context.SaveChanges();
//        var a = _context.Participations.Include(p => p.Respondents);
    }
}