/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Information;

public abstract class InformationBase
{
    public long Id { get; set; }
    public abstract string GetInformation();
    public String StepName { get; set; }
    
    protected InformationBase(long id)
    {
        Id = id;
        StepName = "";
    }

    protected InformationBase()
    {
        Id = default;
        StepName = "";
    }
}