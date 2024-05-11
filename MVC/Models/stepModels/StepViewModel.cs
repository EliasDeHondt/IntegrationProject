using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class StepViewModel
{
    [Required]
    [Range(0, int.MaxValue)]
    public int StepNumber { get; set; }
    [Key]
    public long Id { get; set; }
}