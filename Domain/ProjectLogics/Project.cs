/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.Platform;

namespace Domain.ProjectLogics;

public class Project
{
    [Key]
    public long Id { get; set; }
    [Required]
    public MainTheme MainTheme { get; set; }
    [Required]
    public SharedPlatform SharedPlatform { get; set; }

    public Project(MainTheme mainTheme, SharedPlatform sharedPlatform, long id = 0)
    {
        MainTheme = mainTheme;
        Id = id;
        SharedPlatform = sharedPlatform;
    }

    public Project()
    {
        Id = default;
        MainTheme = new MainTheme();
        SharedPlatform = new SharedPlatform();
    }
}