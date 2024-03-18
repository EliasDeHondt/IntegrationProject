using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class InformationViewModel
{
    [Key]
    public long Id { get; set; }
    [Required]
    public string Information { get; set; }
    public string InformationType { get; set; }
}