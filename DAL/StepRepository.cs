using Data_Access_Layer.DbContext;
using Domain.ProjectLogics.Steps;
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
        return _ctx.Flows.Include(flow => flow.Steps)
            .First(flow => flow.Id == flowId)
            .Steps.First(step => step.StepNumber == stepNumber);
    }
}