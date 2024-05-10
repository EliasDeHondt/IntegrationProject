using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class ChoiceViewModel
{
    [Key]
    public long Id { get; set; }
    [MaxLength(50)]
    public string Text { get; set; }
    public long? NextQuestionId { get; set; }
}