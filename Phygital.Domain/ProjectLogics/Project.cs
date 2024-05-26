/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Domain.FacilitatorFunctionality;
using Domain.Platform;
using Domain.WebApp;

namespace Domain.ProjectLogics;

public class Project
{
    public long Id { get; set; }
    [MaxLength(50)]
    public string Title { get; set; }
    [MaxLength(600)]
    public string Description { get; set; }
    [MaxLength(10485759)]
    public string? Image { get; set; }
    public MainTheme MainTheme { get; set; }
    public SharedPlatform SharedPlatform { get; set; }
    public ICollection<ProjectOrganizer> Organizers { get; set; }
    public Feed Feed { get; set; }
    public bool ProjectClosed { get; set; }
    
    public Project(string title, MainTheme mainTheme, SharedPlatform sharedPlatform, long id = 0)
    {
        Title = title;
        MainTheme = mainTheme;
        Id = id;
        SharedPlatform = sharedPlatform;
        Organizers = new List<ProjectOrganizer>();
        Description = "";
        Feed = new Feed(this);
    }
    
    public Project(string title, MainTheme mainTheme, SharedPlatform sharedPlatform, string description, long id = 0)
    {
        Title = title;
        MainTheme = mainTheme;
        Id = id;
        SharedPlatform = sharedPlatform;
        Organizers = new List<ProjectOrganizer>();
        Description = description;
        Feed = new Feed(this);
    }
    
    public Project()
    {
        Id = default;
        MainTheme = new MainTheme();
        SharedPlatform = new SharedPlatform();
        Organizers = new List<ProjectOrganizer>();
        Description = "";
        Title = "";
        Feed = new Feed(this);
    }
}