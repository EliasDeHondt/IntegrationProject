/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.FacilitatorFunctionality;
using Domain.Platform;

namespace Domain.ProjectLogics;

public class Project
{
    public long Id { get; set; }
    [MaxLength(50)]
    public string Title { get; set; }
    [MaxLength(600)]
    public string Description { get; set; }
    [MaxLength(65000)]
    public string? Image { get; set; }
    public MainTheme MainTheme { get; set; }
    public SharedPlatform SharedPlatform { get; set; }
    public ICollection<ProjectOrganizer> Organizers { get; set; }

    public Project(MainTheme mainTheme, SharedPlatform sharedPlatform, ICollection<ProjectOrganizer> organizers, long id = 0): this(mainTheme.Subject,mainTheme, sharedPlatform, id)
    {
        Organizers = organizers;
        Description = "";
        Title = mainTheme.Subject;
    }

    public Project(string title,MainTheme mainTheme, SharedPlatform sharedPlatform, long id = 0)
    {
        Title = title;
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
        Title = "";
    }

    public Project(string title, string description, SharedPlatform sharedPlatform) 
    {
        Id = default;
        MainTheme = new MainTheme();
        SharedPlatform = new SharedPlatform();
        Organizers = new List<ProjectOrganizer>();
        
        Title = title;
        Description = description;
        SharedPlatform = sharedPlatform;
    }
}