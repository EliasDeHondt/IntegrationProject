/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Business_Layer;

public class StepManager
{
    private readonly StepRepository _repo;

    public StepManager(StepRepository repo)
    {
        _repo = repo;
    }

    public StepBase GetStepForFlowByNumber(long flowId, int stepNumber)
    {
        return _repo.ReadStepForFlowByNumber(flowId, stepNumber);
    }

    public StepBase? GetStepById(long? stepId)
    {
        return _repo.ReadStepById(stepId);
    }

    public Flow GetFlowByNumber(long flowId)
    {
        return _repo.ReadFlowByNumber(flowId);
    }

    public void ChangeStep(StepBase step)
    {
        _repo.UpdateStep(step);
    }

    public IEnumerable<StepBase> GetAllStepsForFlow(long flowId)
    {
        return _repo.ReadAllStepsForFlow(flowId);
    }

    public Choice CreateChoice(long flowId, int stepNr)
    {
        var step = _repo.ReadStepForFlowByNumber(flowId, stepNr);

        Choice choice;

        switch (step)
        {
            case QuestionStep questionStep:
                switch (questionStep.QuestionBase)
                {
                    case ChoiceQuestionBase choiceQuestion:
                        choice = new Choice(" ", choiceQuestion);
                        _repo.CreateChoice(choiceQuestion, choice);
                        return choice;
                }

                break;
        }

        return null;
    }

    public InformationBase CreateInformation(long flowId, int stepNr, string type)
    {
        var step = _repo.ReadStepForFlowByNumber(flowId, stepNr);

        InformationBase information = null;

        switch (step)
        {
            case InformationStep informationStep:
                switch (type)
                {
                    case "Text":
                        information = new Text();
                        break;
                    case "Image":
                        information = new Image();
                        break;
                    case "Video":
                        information = new Video();
                        break;
                }

                _repo.CreateInformation(informationStep, information);
                break;
        }

        return information;
    }

    public Note AddNote(long flowId, int stepNr, string note)
    {
        var step = _repo.ReadStepForFlowByNumber(flowId, stepNr);

        return _repo.CreateNote(step, note);
    }

    public long GetStepId(long flowId, int stepNr)
    {
        return _repo.ReadStepId(flowId, stepNr);
    }

    public InformationBase GetInformationById(long id)
    {
        return _repo.ReadInformationById(id);
    }

    public QuestionBase GetQuestionById(long id)
    {
        return _repo.ReadQuestionById(id);
    }

    public Choice GetChoiceById(long id)
    {
        return _repo.ReadChoiceById(id);
    }

    public InformationBase ChangeInformation(InformationBase information, string content)
    {
        return _repo.UpdateInformation(information, content);
    }

    public void ChangeChoice(Choice choice)
    {
        _repo.UpdateChoice(choice);
    }

    public void ChangeQuestion(QuestionBase question)
    {
        _repo.UpdateQuestion(question);
    }
}