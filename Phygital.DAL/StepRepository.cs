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
            .Single(s => s.Id == stepId);

        return ReadExtendedStep(tempStep);
    }

    public IEnumerable<StepBase> ReadAllStepsForFlow(long flowId)
    {
        var tempSteps = _ctx.Flows
            .AsNoTracking()
            .Include(flow => flow.Steps)
            .Where(flow => flow.Id == flowId)
            .SelectMany(flow => flow.Steps)
            .ToList();

        return tempSteps.Select(ReadExtendedStep);
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

    public InformationBase ReadInformationById(long id)
    {
        var tempInfo = _ctx.Information
            .Single(i => i.Id == id);

        switch (tempInfo)
        {
            case Text text:
                return _ctx.Texts
                    .Single(t => t.Id == text.Id);
            case Video video:
                return _ctx.Videos
                    .Single(v => v.Id == video.Id);
            case Image image:
                return _ctx.Images
                    .Single(i => i.Id == image.Id);
            case Hyperlink link:
                return _ctx.Hyperlinks
                    .Single(l => l.Id == link.Id);
            default: return tempInfo;
        }
    }

    public QuestionBase ReadQuestionById(long id)
    {
        var tempQuestion = _ctx.Questions
            .Single(q => q.Id == id);

        switch (tempQuestion)
        {
            case OpenQuestion open:
                return _ctx.OpenQuestions
                    .Single(o => o.Id == open.Id);
            case RangeQuestion range:
                return _ctx.ChoiceQuestions
                    .Include(r => r.Choices)
                    .Single(r => r.Id == range.Id);
            case SingleChoiceQuestion singleChoice:
                return _ctx.ChoiceQuestions
                    .Include(s => s.Choices)
                    .Single(s => s.Id == singleChoice.Id);
            case MultipleChoiceQuestion multipleChoice:
                return _ctx.ChoiceQuestions
                    .Include(m => m.Choices)
                    .Single(m => m.Id == multipleChoice.Id);
            default: return tempQuestion;
        }
    }

    public Choice ReadChoiceById(long id)
    {
        return _ctx.Choices
            .Include(c => c.NextStep)
            .Single(c => c.Id == id);
    }

    public void UpdateInformation(InformationBase information, string content)
    {
        switch (information)
        {
            case Text text:
                _ctx.Texts
                    .Single(t => t.Id == text.Id).InformationText = content;
                break;
            case Video video:
                _ctx.Videos
                    .Single(v => v.Id == video.Id).FilePath = content;
                break;
            case Image image:
                _ctx.Images
                    .Single(i => i.Id == image.Id).Base64 = content;
                break;
            case Hyperlink link:
                _ctx.Hyperlinks
                    .Single(l => l.Id == link.Id).URL = content;
                break;
        }
    }

    public void UpdateChoice(Choice choice, string text, long? nextStepId)
    {
        var newChoice = _ctx.Choices.Single(c => c.Id == choice.Id);
        newChoice.Text = text;
        if (nextStepId != null)
            newChoice.NextStep = _ctx.Steps.Single(s => s.Id == nextStepId);
    }
    public void UpdateStepNum(QuestionStep step,long? stepNum)
    {
        //_ctx.Steps.Find(step.Id).Single(s => s.StepNumber == stepNum);
    }

    public void UpdateQuestion(QuestionBase question)
    {
        _ctx.Questions.Update(question);
    }
    
    public void UpdateStepsByNumber(StepBase step, StepBase prevstep)
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