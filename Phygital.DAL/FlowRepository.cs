/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer.DbContext;
using Domain.Accounts;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
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
        return _context.Flows
            .AsNoTracking()
            .First(flow => flow.Id == id);
    }
    
    public Flow ReadFlowByIdIncludingTheme(long id)
    {
        return _context.Flows
            .Include(f => f.Theme)
            .Include(f => f.Steps)
            .First(f => f.Id == id);
    }

    public void AddFlow(Flow flow)
    {
        _context.Flows.Add(flow);
    }

    //TODO: This function returns *ALL* flows possible, it does NOT return Project-specific Flows!
    public IEnumerable<Flow> ReadAllFlows()
    {
        return _context.Flows
            .AsNoTracking()
            .ToList();
    }
    
    public void AddParticipationByFlow(long flowId,string email)
    {
        var participations = _context.Participations;
        Participation participation = new Participation(ReadFlowById(flowId));
        participation.Respondents.Add(new Respondent(email,participation)); //respondent
        participations.Add(participation);
    }

    public void UpdateFlowState(Flow flow)
    {
        _context.Flows.Update(flow);
    }

    public void DeleteFlowById(long flowId)
    {
        Flow flow = _context.Flows
            .Include(f => f.Steps)
            .Include(f => f.Participations)
            .FirstOrDefault(f => f.Id == flowId)!;

        ICollection<StepBase> steps = flow.Steps;
        
        foreach (StepBase stepBase in steps)
        {
            long id = stepBase.Id;
            
            var combinedStepToDelete = _context.CombinedSteps.Find(id);
            if (combinedStepToDelete != null)
            {
                _context.CombinedSteps.Remove(combinedStepToDelete);
            }

            var questionStepToDelete = _context.QuestionSteps.Find(id);
            if (questionStepToDelete != null)
            {
                _context.QuestionSteps.Remove(questionStepToDelete);
            }
            
            var informationStepToDelete = _context.InformationSteps.Find(id);
            if (informationStepToDelete != null)
            {
                _context.InformationSteps.Remove(informationStepToDelete);
            }
        }
        
        _context.Participations.RemoveRange(flow.Participations);
        _context.Flows.Remove(flow);
    }
}