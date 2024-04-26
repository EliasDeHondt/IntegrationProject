/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class CombinedStep : StepBase
{
    public InformationBase InformationBase { get; set; }
    public QuestionBase QuestionBase { get; set; }
    
    public CombinedStep(int stepNumber, InformationBase informationBase, QuestionBase questionBase, Flow flow, long id = 0) : 
        base(stepNumber, flow, id)
    {
        InformationBase = informationBase;
        QuestionBase = questionBase;
    }

    public CombinedStep()
    {
        InformationBase = new Text();
        QuestionBase = new SingleChoiceQuestion();
    }
}