using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions.Answers;

public class OpenAnswer : Answer
{
    [MaxLength(300)]
    public string Answer { get; set; }

    public OpenAnswer(QuestionBase questionBase, string answer, long id = 0) : base(questionBase, id)
    {
        Answer = answer;
    }

    public OpenAnswer(QuestionBase questionBase, long id = 0) : base(questionBase, id)
    {
        Answer = string.Empty;
    }

    public OpenAnswer()
    {
        Answer = string.Empty;
    }
}