/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Questions;

public abstract class QuestionBase
{
    [Key]
    public long Id { get; set; }
    [Required]
    public ICollection<Answer> Answers { get; set; }
    [Required]
    [MaxLength(150)]
    public string Question { get; set; }
    
    public abstract object Answer();
    //public abstract ICollection<Choice> GetChoices();
    public ICollection<Choice> Choices { get; set; }

    protected QuestionBase(ICollection<Answer> answers, string question, long id = 0)
    {
        Id = id;
        Answers = answers;
        Question = question;
    }

    protected QuestionBase(string question, long id = 0)
    {
        Id = id;
        Question = question;
        Answers = new List<Answer>();
    }

    protected QuestionBase()
    {
        Id = default;
        Question = string.Empty;
        Answers = new List<Answer>();
    }
}