/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions;

public class OpenQuestion : QuestionBase
{
    [Required]
    [MaxLength(600)]
    public string TextField { get; set; } = string.Empty;

    public OpenQuestion(string question, ICollection<Answer> answers, long id = 0) : base(answers, question, id)
    {
    }
    
    public OpenQuestion(string question, long id = 0) : base(question, id)
    {
    }

    public OpenQuestion()
    {
    }

    public override object Answer()
    {
        throw new NotImplementedException();
    }
    
}