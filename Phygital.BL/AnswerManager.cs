using System.ComponentModel.DataAnnotations;
using Data_Access_Layer;
using Domain.Exceptions;
using Domain.ProjectLogics.Steps.Questions;
using Domain.ProjectLogics.Steps.Questions.Answers;

namespace Business_Layer;

public class AnswerManager
{
    private readonly AnswerRepository _repo;

    public AnswerManager(AnswerRepository repo)
    {
        _repo = repo;
    }


    public Answer AddAnswer(QuestionBase question, string answerText = null)
    {
        Answer answer;
        if (answerText == null)
        {
            answer = new ChoiceAnswer(question);
        }
        else
        {
            answer = new OpenAnswer(question, answerText);
        }
        
        List<ValidationResult> errors = new List<ValidationResult>();

        var newAnswer = ValidateObject(answer, ref errors) ? _repo.CreateAnswer(answer) : throw new CustomValidationException(errors);

        return newAnswer;
    }
    
    
    
    public void AddSelections(ICollection<Selection> selections)
    {
        _repo.CreateSelections(selections);
    }
    
    private bool ValidateObject(object obj,ref List<ValidationResult> errors)
    {
        return Validator.TryValidateObject(obj, new ValidationContext(obj), errors, true);
    }
    
}