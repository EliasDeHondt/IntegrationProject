﻿/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics.Steps.Questions.Answers;

namespace Domain.ProjectLogics.Steps.Questions;

public class RangeQuestion : ChoiceQuestionBase
{
    
    public RangeQuestion(string question, ICollection<Choice> choices, ICollection<ChoiceAnswer> answers, long id = 0) : base(answers, question, choices, id)
    {
    }
    
    public RangeQuestion(string question, ICollection<Choice> choices, long id = 0) : base(question, choices, id)
    {
    }

    public RangeQuestion()
    {
    }
    
    private int SelectOne()
    {
        throw new NotImplementedException();
    }

    public override object Answer()
    {
        throw new NotImplementedException();
    }
}