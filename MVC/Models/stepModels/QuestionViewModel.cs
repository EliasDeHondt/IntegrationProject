using System.ComponentModel.DataAnnotations;
using Domain.ProjectLogics.Steps.Questions;

namespace MVC.Models;

public class QuestionViewModel
{
    [Key]
    public long Id { get; set; }
    [Required]
    public string Question { get; set; }
    public string QuestionType { get; set; }
    public ICollection<Choice> Choices { get; set; }
}