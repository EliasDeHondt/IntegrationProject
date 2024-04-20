using Microsoft.Build.Framework;

namespace MVC.Models.userModels;

public class EmailDto
{
    [Required]
    public string email { get; set; }
}