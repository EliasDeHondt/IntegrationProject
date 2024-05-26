using Domain.ProjectLogics.Steps.Questions;
using Domain.ProjectLogics.Steps.Questions.Answers;

namespace MVC.Models.answerModels;

public class QuestionAnswersDto
{
    public string Question { get; set; }
    public ICollection<string> Answers { get; set; }
}