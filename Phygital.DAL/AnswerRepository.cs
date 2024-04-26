using Data_Access_Layer.DbContext;
using Domain.ProjectLogics.Steps.Questions.Answers;

namespace Data_Access_Layer;

public class AnswerRepository
{
    private readonly CodeForgeDbContext _ctx;

    public AnswerRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }

    public Answer CreateAnswer(Answer answer)
    {
        _ctx.Answers.Add(answer);
        return answer;
    }

    public void AddSelections(IEnumerable<Selection> selections)
    {
        _ctx.Selections.AddRange(selections);
    }
    
}