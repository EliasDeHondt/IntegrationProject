using Data_Access_Layer.DbContext;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Questions;
using Domain.ProjectLogics.Steps.Questions.Answers;
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
    
    public string[] GetChoicesNames(long questionId)
    {
        
        // var a = _ctx.ChoiceQuestions
        //     .Include(q => q.Choices)
        //     .Where(q => q.Question.Trim().Equals(question.Trim())).ToList();
        if (ReadQuestionByName(questionId) is OpenQuestion) //open question
        {
            string[] help = { "0", "0", "0" };
            return null;
        }
        var b = _ctx.ChoiceQuestions
            .Include(q => q.Choices);
        var c = b.Include(c => c.Choices);
        foreach (var questionA in c)
        {
            if (questionA.Id == questionId)
            {
                var choices = questionA.Choices;
                return choices.Select(i => i.Text.ToString()).ToArray();
            }
        }
        return null!;
        
    }
    
    public ChoiceQuestionBase ReadQuestionByName(long question)
    {
        // var b = _ctx.Answers
        //     .Include(a => ((ChoiceAnswer)a).QuestionBase)
        //     .Include(a => ((ChoiceAnswer)a).QuestionBase.Choices);
        // var b = _ctx.ChoiceQuestions
        //     .Include(msq => msq.Answers)
        //     .Include(msq => msq.Choices).Single();
        // var a = _ctx.ChoiceQuestions
        //     .Include(mcq => mcq.Choices)
        //     .Single(mcq => mcq.Question == question);
            // .Include(msq => msq.Answers)
            // .AsNoTracking()
            // .First(q => q.Question == question);
        var a =   _ctx.ChoiceQuestions
            .Include(q => q.Answers)
            .ThenInclude(a => a.Answers)
            .ThenInclude(s => s.Choice)
            .Include(q => q.Choices)
            .ToList();

        foreach (var questionA in a)
        {
            if (questionA.Id == question)
            {
                return questionA;
            }
        }
        return null;
        // var b = a.Single(q => q.Question == question);
        // return b;
    }
    
    public string[] GetAnswerCountsForQuestions(long question)
    {
        var q = ReadQuestionByName(question); //enkel voor ChoiceQuestions, geen open quesions!
        var answerCountsPerQuestion = new List<int>();

        if (q is OpenQuestion)
        {
            return null;
        }
        foreach (var choice in q.Choices)
        {
            int newChoice = 0;
            foreach (var answer in q.Answers)
            {
                foreach (var selection in answer.Answers)
                {
                    if (choice.Text == selection.Choice.Text)
                    {
                        newChoice++;
                    }
                }
            }
            answerCountsPerQuestion.Add(newChoice);
        }
        
        return answerCountsPerQuestion.Select(i => i.ToString()).ToArray();
    }
}