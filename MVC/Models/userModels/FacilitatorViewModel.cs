using System.ComponentModel.DataAnnotations;

namespace MVC.Models.userModels;

public class FacilitatorViewModel
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
    public IEnumerable<long> ProjectIds { get; set; }
    [Required]
    public long platformId { get; set;  }
}