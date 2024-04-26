/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Information;

public class Text : InformationBase
{
    [MaxLength(600)]
    public string InformationText { get; set; }
    
    public Text(string informationText, long id = 0) : base(id)
    {
        InformationText = informationText;
    }

    public Text()
    {
        InformationText = string.Empty;
    }

    public override string GetInformation()
    {
        return InformationText;
    }
}