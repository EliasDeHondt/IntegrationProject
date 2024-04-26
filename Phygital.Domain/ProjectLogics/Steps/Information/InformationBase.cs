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
    
    protected InformationBase(long id)
    {
        Id = id;
    }

    protected InformationBase()
    {
        Id = default;
    }
}