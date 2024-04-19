/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.Accounts;
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
    [Required]
    public ICollection<Facilitator> Facilitators { get; set; }

    public Project(MainTheme mainTheme, SharedPlatform sharedPlatform, ICollection<Facilitator> facilitators, long id = 0): this(mainTheme, sharedPlatform, id)
    {
        Facilitators = facilitators;
    }

    public Project(MainTheme mainTheme, SharedPlatform sharedPlatform, long id = 0)
    {
        MainTheme = mainTheme;
        Id = id;
        SharedPlatform = sharedPlatform;
        Facilitators = new List<Facilitator>();
    }
    
    public Project()
    {
        Id = default;
        MainTheme = new MainTheme();
        SharedPlatform = new SharedPlatform();
        Facilitators = new List<Facilitator>();
    }
}