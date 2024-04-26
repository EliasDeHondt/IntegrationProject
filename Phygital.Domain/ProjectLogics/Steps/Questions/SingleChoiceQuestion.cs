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
        Choices = choices;
    }
    
    public SingleChoiceQuestion(string question, ICollection<Choice> choices, long id = 0) : base(question, choices, id)
    {
        Choices = choices;
    }
    
    public SingleChoiceQuestion()
    {
        Choices = new List<Choice>();
    }

}