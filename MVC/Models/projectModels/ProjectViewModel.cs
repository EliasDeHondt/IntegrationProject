using Domain.Platform;
using Microsoft.Build.Framework;

namespace MVC.Models.projectModels;

public class ProjectViewModel
{
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public long SharedPlatformId { get; set; }
    
}