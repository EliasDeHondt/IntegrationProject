namespace MVC.Models;

public class AnswerDto
{
    public ICollection<string> Answers { get; set; }
    public string AnswerText { get; set; } = null;
}