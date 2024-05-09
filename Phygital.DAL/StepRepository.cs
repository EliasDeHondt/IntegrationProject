using Data_Access_Layer.DbContext;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
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

    public StepBase ReadExtendedStep(StepBase stepBase)
    {
        switch (stepBase)
        {
            case InformationStep i:
                return _ctx.InformationSteps
                    .Include(step => step.InformationBases)
                    .First(step => step.Id == i.Id);
            case QuestionStep q:
                var questionStep = _ctx.QuestionSteps
                    .Include(step => step.QuestionBase)
                    .First(step => step.Id == q.Id);

                if (questionStep.QuestionBase is ChoiceQuestionBase cqBase)
                {
                    _ctx.Entry(cqBase)
                        .Collection(qb => qb.Choices)
                        .Load();
                }

                return questionStep;
            case CombinedStep c:
                return _ctx.CombinedSteps
                    .Include(step => step.InformationBase)
                    .Include(step => step.QuestionBase)
                    .First(step => step.Id == c.Id);
            default: return stepBase;
        }
    }

    public StepBase ReadStepForFlowByNumber(long flowId, int stepNumber)
    {
        StepBase tempStep = _ctx.Flows
            .AsNoTracking()
            .Include(flow => flow.Steps)
            .First(flow => flow.Id == flowId)
            .Steps.First(step => step.StepNumber == stepNumber);

        return ReadExtendedStep(tempStep);
    }

    public IEnumerable<StepBase> ReadAllStepsForFlow(long flowId)
    {
        IEnumerable<StepBase> tempSteps = _ctx.Flows
            .AsNoTracking()
            .Include(flow => flow.Steps)
            .First(flow => flow.Id == flowId)
            .Steps;

        List<StepBase> steps = new List<StepBase>();

        foreach (var step in tempSteps)
        {
            steps.Add(ReadExtendedStep(step));
        }

        return steps;
    }

    public Flow ReadFlowByNumber(long flowId)
    {
        return _ctx.Flows
            .Include(flow => flow.Steps)
            .First(flow => flow.Id == flowId);
    }

    public void AddStep(StepBase step)
    {
        switch (step)
        {
            case InformationStep i:
                _ctx.InformationSteps.Add(i);
                break;
            case QuestionStep q:
                _ctx.QuestionSteps.Add(q);
                break;
            case CombinedStep c:
                _ctx.CombinedSteps.Add(c);
                break;
        }
    }

    public void UpdateStep(StepBase step)
    {
        switch (step)
        {
            case InformationStep i:
                _ctx.InformationSteps.Update(i);
                break;
            case QuestionStep q:
                _ctx.QuestionSteps.Update(q);
                break;
            case CombinedStep c:
                _ctx.CombinedSteps.Update(c);
                break;
        }
    }
}