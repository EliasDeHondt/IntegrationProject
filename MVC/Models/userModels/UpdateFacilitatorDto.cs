using System.ComponentModel.DataAnnotations;

namespace MVC.Models.userModels;

public class UpdateFacilitatorDto
{
    [Required]
    [MinLength(1)]
    public string UserName { get; set; }
    [Required]
    [MinLength(1)]
    public string Email { get; set; }
    [Required]
    public IEnumerable<long> AddedProjects { get; set; }
    [Required]
    public IEnumerable<long> RemovedProjects { get; set; }
}