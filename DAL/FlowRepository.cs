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
        var participations = _context.Flows.Find(flowId).Participations;
        var name = email.Substring(0, email.IndexOf('@')); //aa.ww@email.com --> aa.ww
        Respondent respondent = new Respondent(email);

        Participation participation = new Participation(_context.Flows.Find(flowId));
        participation.Respondents.Add(respondent); //respondent
        participations.Add(participation);
    }

    public void UpdateFlowState(Flow flow)
    {
        _context.Flows.Update(flow);
        _context.SaveChanges();
    }
}