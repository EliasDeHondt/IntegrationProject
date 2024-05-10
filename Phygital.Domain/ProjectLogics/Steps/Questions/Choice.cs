/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics.Steps.Questions.Answers;

namespace Domain.ProjectLogics.Steps.Questions;

public class Choice
{
    public long Id { get; set; }
    [MaxLength(50)]
    public string Text { get; set; }
    public ChoiceQuestionBase QuestionBase { get; set; }
    public ICollection<Selection> Selections { get; set; }
    
    public QuestionBase? NextQuestionBase { get; set; }

    public Choice(string text, ChoiceQuestionBase questionBase, ICollection<Selection> selections, long id = 0)
    {
        Id = id;
        Text = text;
        QuestionBase = questionBase;
        Selections = selections;
    }

    public Choice(string text, ChoiceQuestionBase questionBase, QuestionBase nextQuestionBase, long id = 0)
    {
        Id = id;
        Text = text;
        QuestionBase = questionBase;
        Selections = new List<Selection>();
        NextQuestionBase = nextQuestionBase;
    }

    public Choice(string text, ChoiceQuestionBase questionBase, long id = 0)
    {
        Id = id;
        Text = text;
        QuestionBase = questionBase;
        Selections = new List<Selection>();
    }

    public Choice()
    {
        Id = default;
        Text = string.Empty;
        QuestionBase = new SingleChoiceQuestion();
        Selections = new List<Selection>();
    }
}