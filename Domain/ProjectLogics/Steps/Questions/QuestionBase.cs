/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics.Steps.Questions.Answers;

namespace Domain.ProjectLogics.Steps.Questions;

public abstract class QuestionBase
{
    [Key]
    public long Id { get; set; }
    [Required]
    public ICollection<ChoiceAnswer> Answers { get; set; }
    [Required]
    [MaxLength(600)]
    public string Question { get; set; }
    
    public abstract object Answer();

    protected QuestionBase(ICollection<ChoiceAnswer> answers, string question, long id = 0)
    {
        Id = id;
        Answers = answers;
        Question = question;
    }

    protected QuestionBase(string question, long id = 0)
    {
        Id = id;
        Question = question;
        Answers = new List<ChoiceAnswer>();
    }

    protected QuestionBase()
    {
        Id = default;
        Question = string.Empty;
        Answers = new List<ChoiceAnswer>();
    }
}