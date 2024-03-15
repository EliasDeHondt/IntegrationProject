/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions;

public class SingleChoiceQuestion : QuestionBase
{
    [Required]
    public ICollection<Choice> Choices { get; set; }

    public SingleChoiceQuestion(string question, ICollection<Choice> choices, ICollection<Answer> answers, long id = 0) : base(answers, question, id)
    {
        Choices = choices;
    }
    
    public SingleChoiceQuestion(string question, ICollection<Choice> choices, long id = 0) : base(question, id)
    {
        Choices = choices;
    }
    
    public SingleChoiceQuestion()
    {
        Choices = new List<Choice>();
    }
    
    private string SelectOne()
    {
        throw new NotImplementedException();
    }

    public override object Answer()
    {
        throw new NotImplementedException();
    }

    /*public override ICollection<Choice> GetChoices()
    {
        return Choices;
    }*/
}