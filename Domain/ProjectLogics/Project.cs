/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics;

public class Project
{
    [Key]
    public long Id { get; set; }
    [Required]
    public MainTheme MainTheme { get; set; }

    public Project(MainTheme mainTheme, long id = 0)
    {
        MainTheme = mainTheme;
        Id = id;
    }

    public Project()
    {
        Id = default;
        MainTheme = new MainTheme();
    }
}