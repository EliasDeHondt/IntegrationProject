/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics.Steps.Questions.Answers;
using Domain.Statistics;
using FileHelpers;

namespace Domain.ProjectLogics.Steps.Questions;
[FixedLengthRecord]
public abstract class QuestionBase
{
    [Key]
    [FieldFixedLength(length:100)]
    public long Id { get; set; }
    [Required]
    [FieldConverter(typeof(CollectionConverter<ChoiceAnswer>))]
    public ICollection<ChoiceAnswer> Answers { get; set; }
    [Required]
    [MaxLength(600)]
    public string Question { get; set; }
    

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