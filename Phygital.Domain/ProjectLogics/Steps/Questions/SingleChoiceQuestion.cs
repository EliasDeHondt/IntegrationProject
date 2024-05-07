/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/


using Domain.ProjectLogics.Steps.Questions.Answers;

namespace Domain.ProjectLogics.Steps.Questions;

public class SingleChoiceQuestion : ChoiceQuestionBase
{

    public SingleChoiceQuestion(string question, ICollection<Choice> choices, ICollection<ChoiceAnswer> answers, long id = 0) : base(answers, question, choices, id)
    {
    }

    public SingleChoiceQuestion(string question, long id = 0) : base(question, new List<Choice>(), id)
    {
    }
    
    public SingleChoiceQuestion()
    {
        Choices = new List<Choice>();
    }

}