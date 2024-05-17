using Data_Access_Layer.DbContext;
using Domain.FacilitatorFunctionality;
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
                    .Single(step => step.Id == i.Id);
            case QuestionStep q:
                var questionStep = _ctx.QuestionSteps
                    .Include(step => step.QuestionBase)
                    .Single(step => step.Id == q.Id);

                if (questionStep.QuestionBase is ChoiceQuestionBase cqBase)
                {
                    _ctx.Entry(cqBase)
                        .Collection(qb => qb.Choices)
                        .Query()
                        .Include(choice => choice.NextStep)
                        .Load();
                }

                return questionStep;
            case CombinedStep c:
                return _ctx.CombinedSteps
                    .Include(step => step.InformationBase)
                    .Include(step => step.QuestionBase)
                    .Single(step => step.Id == c.Id);
            default: return stepBase;
        }
    }

    public StepBase ReadStepForFlowByNumber(long flowId, int stepNumber)
    {
        StepBase tempStep = _ctx.Flows
            .AsNoTracking()
            .Include(flow => flow.Steps)
            .Where(flow => flow.Id == flowId)
            .SelectMany(flow => flow.Steps)
            .Single(step => step.StepNumber == stepNumber);

        return ReadExtendedStep(tempStep);
    }

    public StepBase ReadStepById(long? stepId)
    {
        StepBase tempStep = _ctx.Steps
            .AsNoTracking()
            .Include(s => s.Flow)
            .Single(s => s.Id == stepId);

        return ReadExtendedStep(tempStep);
    }

    public IEnumerable<StepBase> ReadAllStepsForFlow(long flowId)
    {
        IEnumerable<StepBase> tempSteps = _ctx.Flows
            .AsNoTracking()
            .Include(flow => flow.Steps)
            .Single(flow => flow.Id == flowId)
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
            .Single(flow => flow.Id == flowId);
    }

    public void CreateStep(StepBase step)
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

    public void CreateChoice(ChoiceQuestionBase question, Choice choice)
    {
        _ctx.ChoiceQuestions.Find(question.Id)!.Choices.Add(choice);
    }

    public void CreateInformation(InformationStep step, InformationBase information)
    {
        _ctx.InformationSteps.Find(step.Id)!.InformationBases.Add(information);
    }

    public Note CreateNote(StepBase step, string note)
    {
        var newNote = new Note(note);
        _ctx.Steps.Find(step.Id)!.Notes.Add(newNote);
        return newNote;
    }

    public long ReadStepId(long flowId, int stepNr)
    {

        return _ctx.Flows
            .AsNoTracking()
            .Where(flow => flow.Id == flowId)
            .SelectMany(flow => flow.Steps)
            .Where(step => step.StepNumber == stepNr)
            .Select(step => step.Id)
            .Single();
    }

    public void UpdateInformation(InformationBase information)
    {
        _ctx.Information.Update(information);
    }

    public void UpdateChoice(Choice choice)
    {
        _ctx.Choices.Update(choice);
    }

    public void UpdateQuestion(QuestionBase question)
    {
        _ctx.Questions.Update(question);
    }
}