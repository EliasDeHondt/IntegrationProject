/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions;

public class MultipleChoiceQuestion : QuestionBase
{
    [Required]
    public ICollection<Choice> Choices { get; set; }

    public MultipleChoiceQuestion(string question, ICollection<Choice> choices, ICollection<Answer> answers, long id = 0) : base(answers, question, id)
    {
        Choices = choices;
    }

    public MultipleChoiceQuestion(string question, ICollection<Choice> choices, long id = 0) : base(question, id)
    {
        Choices = choices;
    }

    public MultipleChoiceQuestion()
    {
        Choices = new List<Choice>();
    }
    
    private IEnumerable<string> SelectMultiple()
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