using Data_Access_Layer.DbContext;
using Domain.ProjectLogics.Steps.Questions;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer;

public class QuestionRepository
{
    
    private readonly CodeForgeDbContext _ctx;
    
    public QuestionRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }

    public QuestionBase ReadQuestionFromStep(long flowId, int stepNumber)
    {
        return _ctx.QuestionSteps
            .Include(qs => qs.QuestionBase)
            .Where(qs => qs.StepNumber == stepNumber)
            .Single(qs => qs.Flow.Id == flowId)
            .QuestionBase;
    }

    public IEnumerable<Choice> ReadChoicesForQuestion(long questionId)
    {
        return _ctx.ChoiceQuestions
            .Include(mcq => mcq.Choices)
            .Single(mcq => mcq.Id == questionId)
            .Choices
            .ToList();
    }
}