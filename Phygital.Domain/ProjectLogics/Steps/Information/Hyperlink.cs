/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Information;

public class Hyperlink : InformationBase
{
    [MaxLength(600)]
    public string URL { get; set; }

    public Hyperlink(string url, long id = 0) : base(id)
    {
        URL = url;
    }

    public Hyperlink()
    {
        URL = string.Empty;
    }

    public override string GetInformation()
    {
        return URL;
    }
}