using Domain.Platform;
using Microsoft.Build.Framework;

namespace MVC.Models;

public class ProjectViewModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public long SharedPlatformId { get; set; }
}