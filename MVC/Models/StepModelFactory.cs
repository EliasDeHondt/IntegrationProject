/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace MVC.Models;

public static class StepModelFactory
{

    public static TViewModel CreateStepViewModel<TViewModel, TStep>(TStep step)
        where TViewModel : StepViewModel where TStep : StepBase
    {
        switch (step)
        {
            case CombinedStep cStep:
                return CreateCombinedStepViewModel(cStep) as TViewModel;
            case InformationStep iStep:
                return CreateInformationStepViewModel(iStep) as TViewModel;
            case QuestionStep qStep:
                return CreateQuestionStepViewModel(qStep) as TViewModel;
            default:
                return null;
            
        }
    }
    
    private static CombinedStepViewModel CreateCombinedStepViewModel(CombinedStep step)
    {
        return new CombinedStepViewModel
        {
            Id = step.Id,
            InformationViewModel = CreateInformationViewModel(step.InformationBase),
            StepNumber = step.StepNumber
        };
    }
    
    private static InformationStepViewModel CreateInformationStepViewModel(InformationStep step)
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
            case OpenQuestion:
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