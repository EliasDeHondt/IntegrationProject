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
using Domain.ProjectLogics.Steps.Questions;
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

    public IEnumerable<Flow> ReadAllFlowsByType(string type)
    {
        if (type.Equals("linear"))
            return _context.Flows.AsNoTracking().Where(flow => flow.FlowType.Equals(FlowType.Linear)).ToList();
        if (type.Equals("circular"))
            return _context.Flows.AsNoTracking().Where(flow => flow.FlowType.Equals(FlowType.Circular)).ToList();
        return ReadAllFlows();
    }

    public IEnumerable<Flow> ReadAllFlowsWithTheme()
    {
        return _context.Flows
            .Include(f => f.Theme)
            .AsNoTracking()
            .ToList();
    }

    public void AddParticipationByFlow(long flowId, string email)
    {
        var participations = _context.Participations;
        Participation participation = new Participation(ReadFlowById(flowId));
        participation.Respondents.Add(new Respondent(email, participation)); //respondent
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

        foreach (var step in flow.Steps)
        {
            switch (step)
            {
                case InformationStep i:
                    DeleteInformationStep(i);
                    break;
                case QuestionStep q:
                    DeleteQuestionStep(q);
                    break;
                case CombinedStep c:
                    DeleteCombinedStep(c);
                    break;
            }
        }

        _context.Steps.RemoveRange(flow.Steps);
        _context.Participations.RemoveRange(flow.Participations);
        _context.Flows.Remove(flow);
    }

    private void DeleteAnswers(QuestionBase q)
    {
        _context.Entry(q).Collection(question => question.Answers).Load();
        _context.Answers.RemoveRange(q.Answers);
    }

    private void DeleteChoices(ChoiceQuestionBase q)
    {
        _context.Entry(q).Collection(question => question.Choices).Load();
        _context.Choices.RemoveRange(q.Choices);
    }

    private void DeleteInformationStep(InformationStep i)
    {
        _context.Entry(i).Reference(istep => istep.InformationBase).Load();
        _context.Steps.Remove(i);
        _context.Information.Remove(i.InformationBase);
    }

    private void DeleteQuestionStep(QuestionStep q)
    {
        _context.Entry(q).Reference(qstep => qstep.QuestionBase).Load();
        _context.Steps.Remove(q);
        DeleteAnswers(q.QuestionBase);
        if (q.QuestionBase is ChoiceQuestionBase cq) DeleteChoices(cq);
        _context.Questions.Remove(q.QuestionBase);
    }

    private void DeleteCombinedStep(CombinedStep c)
    {
        _context.Entry(c).Reference(cstep => cstep.InformationBase).Load();
        _context.Entry(c).Reference(cstep => cstep.QuestionBase).Load();
        _context.Steps.Remove(c);
        DeleteAnswers(c.QuestionBase);
        if (c.QuestionBase is ChoiceQuestionBase cq) DeleteChoices(cq);
        _context.Information.Remove(c.InformationBase);
        _context.Questions.Remove(c.QuestionBase);
    }

    public IEnumerable<StepBase> GetAllSteps(long flowId)
    {
        Flow flow = _context.Flows
            .AsNoTracking()
            .Include(flow => flow.Steps)
            .First(flow => flow.Id == flowId);

        return flow.Steps;
    }

    public void AddStepToFlow(long flowId, StepBase step)
    {
        _context.Flows.Find(flowId)!.Steps.Add(step);
    }


    public string[] GetCountStepsPerFlow()
    {
        var flows = ReadAllFlows();
        var stepsPerFlow = new List<int>();

        foreach (var flow in flows)
        {
            var stepsCount = GetAllSteps(flow.Id);
            var v = stepsCount.Count();
            stepsPerFlow.Add(v);
        }
        var a =  stepsPerFlow.Select(count => count.ToString()).ToArray();
        return a;
    }
    public string[] GetNamesPerFlow()
    {
        var flows = ReadAllFlows();
        var namesPerFlow = new List<string>();

        foreach (var flow in flows)
        {
            var name = ReadFlowByIdIncludingTheme(flow.Id);
            namesPerFlow.Add(name.Theme.Subject);
        }
        var a =  namesPerFlow.Select(count => count.ToString()).ToArray();
        return a;
    }
}