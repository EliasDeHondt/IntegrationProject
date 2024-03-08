using Data_Access_Layer.DbContext;
using Domain.ProjectLogics.Steps;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer;

public class StepRepository
{

    private readonly CodeForgeDbContext _ctx;
    
    public StepRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }

    public IStep Read(long id)
    {
        return _ctx.Steps.Find(id);
    }
    
    public IStep ReadStepForFlowByNumber(long flowId, int stepNumber)
    {
        return _ctx.Steps.FirstOrDefault(step => step.StepNumber == stepNumber && step.Flow.Id == flowId);
    }
    
}