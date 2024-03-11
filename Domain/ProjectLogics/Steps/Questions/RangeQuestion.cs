/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions;

public class RangeQuestion : QuestionBase
{
    [Required]
    public ICollection<Choice> Choices { get; set; }
    
    public RangeQuestion(string question, ICollection<Choice> choices, ICollection<Answer> answers, long id = 0) : base(answers, question, id)
    {
        Choices = choices;
    }
    
    public RangeQuestion(string question, ICollection<Choice> choices, long id = 0) : base(question, id)
    {
        Choices = choices;
    }

    public RangeQuestion()
    {
        Choices = new List<Choice>();
    }
    private int SelectOne()
    {
        throw new NotImplementedException();
    }

    public override object Answer()
    {
        throw new NotImplementedException();
    }
    
    public override IEnumerable<string> GetChoices()
    {
        throw new NotImplementedException();
    }
}