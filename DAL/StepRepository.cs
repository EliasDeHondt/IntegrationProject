using Data_Access_Layer.DbContext;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Microsoft.EntityFrameworkCore;
/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

namespace Data_Access_Layer;

public class StepRepository
{

    private readonly CodeForgeDbContext _ctx;
    
    public StepRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }

    public StepBase Read(long id, Type stepType)
    { 
        if(stepType == typeof(InformationStep))
        {
            return _ctx.InformationSteps.Find(id);
        }

        if (stepType == typeof(CombinedStep))
        {
            return _ctx.CombinedSteps.Find(id);
        }

        if (stepType == typeof(QuestionStep))
        {
            return _ctx.QuestionSteps.Find(id);
        }

        return null;
    }
    
    public StepBase ReadStepForFlowByNumber(long flowId, int stepNumber)
    {
        StepBase tempStep = _ctx.Flows.Include(flow => flow.Steps)
            .First(flow => flow.Id == flowId)
            .Steps.First(step => step.StepNumber == stepNumber);

        switch (tempStep)
        {
            case InformationStep i: 
                return _ctx.InformationSteps.Include(step => step.InformationBase).First(step => step.Id == i.Id); 
            case QuestionStep q:
                return _ctx.QuestionSteps.Include(step => step.QuestionBase).ThenInclude(question => question.Choices).First(step => step.Id == q.Id);
            case CombinedStep c:
                return _ctx.CombinedSteps.Include(step => step.InformationBase).Include(step => step.QuestionBase)
                    .First(step => step.Id == c.Id);
            default: return null;
        }
    }
}