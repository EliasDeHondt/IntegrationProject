using System.ComponentModel.DataAnnotations;

namespace MVC.Models.userModels;

public class AdminViewModel
{
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    [Required]
    [MinLength(1)]
    public string Email { get; set; }
    [Required]
    [MinLength(10)]
    public string Password { get; set; }
    [Required]
    public long PlatformId { get; set; }
    [Required]
    public List<string> Permissions { get; set; }
}