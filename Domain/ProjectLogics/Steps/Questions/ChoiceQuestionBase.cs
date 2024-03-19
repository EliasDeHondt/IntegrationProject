using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics.Steps.Questions.Answers;

namespace Domain.ProjectLogics.Steps.Questions;

public abstract class ChoiceQuestionBase : QuestionBase
{
    [Required]
    public ICollection<Choice> Choices { get; set; }
   
    protected ChoiceQuestionBase(ICollection<ChoiceAnswer> answers, string question, ICollection<Choice> choices, long id = 0) : base(answers, question, id)
    {
        Choices = choices;
    }

    protected ChoiceQuestionBase(string question, ICollection<Choice> choices, long id = 0) : base(question, id)
    {
        Choices = choices;
    }

    protected ChoiceQuestionBase()
    {
        Choices = new List<Choice>();
    }
    
    public abstract override object Answer();
    
}