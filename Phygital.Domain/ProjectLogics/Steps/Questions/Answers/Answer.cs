using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions.Answers;

public abstract class Answer
{
    public long Id { get; set; }
    public QuestionBase QuestionBase { get; set; }

    protected Answer(QuestionBase questionBase, long id = 0)
    {
        Id = id;
        QuestionBase = questionBase;
    }

    protected Answer()
    {
        Id = default;
        QuestionBase = new SingleChoiceQuestion();
    }
    
    
}