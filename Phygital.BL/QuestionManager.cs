using Data_Access_Layer;
using Domain.ProjectLogics.Steps.Questions;
using Domain.ProjectLogics.Steps.Questions.Answers;

namespace Business_Layer;

public class QuestionManager
{
    private readonly QuestionRepository _repo;

    public QuestionManager(QuestionRepository repo)
    {
        _repo = repo;
    }
    
    public QuestionBase GetQuestionByStep(long flowId, int stepNumber)
    {
        return _repo.ReadQuestionFromStep(flowId, stepNumber);
    }
    
    public QuestionBase GetQuestionById(long id)
    {
        return _repo.ReadQuestionById(id);
    }

    public IEnumerable<Choice> GetChoicesForQuestion(long questionId)
    {
        return _repo.ReadChoicesForQuestion(questionId);
    }

    public string[] GetChoicesNames(long question)
    {
        return _repo.GetChoicesNames(question);
    }
    public string[] GetAnswerCountsForQuestions(long question)
    {
        return _repo.GetAnswerCountsForQuestions(question);
    }

    public string[] GetAnswersFromQuestion(long questionId)
    {
        return _repo.ReadAnswersFromQuestion(questionId);
    }

    public string GetQuestionText(long questionId)
    {
        return _repo.ReadQuestionText(questionId);
    }

    public string GetQuestionType(long questionId)
    {
        return _repo.ReadQuestionType(questionId);
    }
}