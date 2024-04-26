/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class InformationStep : StepBase
{
    public InformationBase InformationBase { get; set; }
    
    public InformationStep(int stepNumber, InformationBase informationBase, Flow flow, long id = 0) : base(stepNumber, flow, id)
    {
        InformationBase = informationBase;
    }

    public InformationStep()
    {
        InformationBase = new Text();
    }
}