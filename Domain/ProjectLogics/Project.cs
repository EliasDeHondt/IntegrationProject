/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.Accounts;
using Domain.FacilitatorFunctionality;
using Domain.Platform;

namespace Domain.ProjectLogics;

public class Project
{
    [Key]
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    [Required]
    public MainTheme MainTheme { get; set; }
    [Required]
    public SharedPlatform SharedPlatform { get; set; }
    [Required]
    public ICollection<ProjectOrganizer> Organizers { get; set; }

    public Project(MainTheme mainTheme, SharedPlatform sharedPlatform, ICollection<ProjectOrganizer> organizers, long id = 0): this(mainTheme, sharedPlatform, id)
    {
        Organizers = organizers;
        Description = "";
    }

    public Project(MainTheme mainTheme, SharedPlatform sharedPlatform, long id = 0)
    {
        MainTheme = mainTheme;
        Id = id;
        SharedPlatform = sharedPlatform;
        Organizers = new List<ProjectOrganizer>();
        Description = "";
    }
    
    public Project()
    {
        Id = default;
        MainTheme = new MainTheme();
        SharedPlatform = new SharedPlatform();
        Organizers = new List<ProjectOrganizer>();
        Description = "";
    }

    public Project(string title, string description, SharedPlatform sharedPlatform)
    {
        Title = title;
        Description = description;
        SharedPlatform = sharedPlatform;
    }
}