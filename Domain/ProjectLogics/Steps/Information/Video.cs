/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Information;

public class Video : InformationBase
{
    [Required]
    [MaxLength(200)]
    public string FilePath { get; set; }

    public Video(string filePath, long id = 0) : base(id)
    {
        FilePath = filePath;
    }

    public Video()
    {
        FilePath = string.Empty;
    }
    

    public override string GetInformation()
    {
        return FilePath;
    }
}