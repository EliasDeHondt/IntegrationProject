/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions.Answers;

public class ChoiceAnswer : Answer
{
    [Required]
    public ICollection<Selection> Answers { get; set; }
    
    
    public ChoiceAnswer(QuestionBase questionBase, ICollection<Selection> answers, long id = 0) : base(questionBase, id)
    {
        Answers = answers;
    }

    public ChoiceAnswer(QuestionBase questionBase, long id = 0) : base(questionBase, id)
    {
        Answers = new List<Selection>();
    }
    
    public ChoiceAnswer()
    {
        Answers = new List<Selection>();
    }
}