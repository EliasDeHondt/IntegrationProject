using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace MVC.Models;

public class StepModelFactory
{
    
    public static CombinedStepViewModel CreateCombinedStepViewModel(CombinedStep step)
    {
        return new CombinedStepViewModel
        {
            Id = step.Id,
            InformationViewModel = CreateInformationViewModel(step.InformationBase),
            StepNumber = step.StepNumber
        };
    }
    
    public static InformationStepViewModel CreateInformationStepViewModel(InformationStep step)
    {
        return new InformationStepViewModel
        {
            Id = step.Id,
            InformationViewModel = CreateInformationViewModel(step.InformationBase),
            StepNumber = step.StepNumber
        };
    }

   private static InformationViewModel CreateInformationViewModel(InformationBase information)
    {
        return new InformationViewModel
        {
            Id = information.Id,
            Information = information.GetInformation(),
            InformationType = information.GetType().Name
        };
    }

    public static QuestionStepViewModel CreateQuestionStepViewModel(QuestionStep step)
    {
        return new QuestionStepViewModel
        {
            Id = step.Id,
            QuestionViewModel = CreateQuestionViewModel(step.QuestionBase),
            StepNumber = step.StepNumber
        };
    }

    private static QuestionViewModel CreateQuestionViewModel(QuestionBase question)
    {
        switch (question)
        {
            case OpenQuestion oQ:
                return new QuestionViewModel
                {
                    Id = question.Id,
                    Question = question.Question,
                    QuestionType = question.GetType().Name
                };
            case ChoiceQuestionBase cQ:
                return new QuestionViewModel
                {
                    Id = question.Id,
                    Question = question.Question,
                    QuestionType = question.GetType().Name,
                    Choices = cQ.Choices
                };
            default: return new QuestionViewModel();
        }
    }
    
}