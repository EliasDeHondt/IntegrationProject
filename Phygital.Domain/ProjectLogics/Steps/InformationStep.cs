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
    public ICollection<InformationBase> InformationBases { get; set; }
    
    public InformationStep(int stepNumber, ICollection<InformationBase> informationBases, Flow flow, long id = 0) : base(stepNumber, flow, id)
    {
        InformationBases = informationBases;
    }    
    
    public InformationStep(int stepNumber, ICollection<InformationBase> informationBases, Flow flow, bool visible, long id = 0) : base(stepNumber, flow, visible, id)
    {
        InformationBases = informationBases;
    }

    public InformationStep()
    {
        InformationBases = new List<InformationBase>();
    }
}