using Domain.Platform;
using Microsoft.Build.Framework;

namespace MVC.Models;

public class ProjectViewModel
{
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public string Image { get; set; }
    [Required]
    public long SharedPlatformId { get; set; }
    
}