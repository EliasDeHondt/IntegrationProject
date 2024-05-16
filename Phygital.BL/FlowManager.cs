/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.Collections;
using Data_Access_Layer;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Business_Layer;

public class FlowManager
{
    private readonly FlowRepository _repository;

    public FlowManager(FlowRepository repository)
    {
        _repository = repository;
    }

    //This function makes a GLOBAL Flow, not project specific!
    public Flow Add(FlowType type)
    {

        Flow newFlow = new Flow
        {
            FlowType = type
        };

        _repository.AddFlow(newFlow);

        return newFlow;
    }
    
    public Flow GetFlowById(long id)
    {
        return _repository.ReadFlowById(id);
    }

    /** Returns all steps in the given flow.
     * 
     * parameter: flowId -> The ID of the flow from which we are getting the steps.
     */ 
    public IEnumerable<StepBase> GetAllStepsInFlow(long flowId)
    {
        return _repository.GetAllSteps(flowId);
    }

    public Flow GetFlowByIdWithTheme(long id)
    {
        return _repository.ReadFlowByIdIncludingTheme(id);
    }
    
    public IEnumerable<Flow> GetAllFlows()
    {
        return _repository.ReadAllFlows();
    }

    public IEnumerable<Flow> GetAllFlowsByType(string type)
    {
        return _repository.ReadAllFlowsByType(type);
    }

    public IEnumerable<Flow> GetAllFlowsWithTheme()
    {
        return _repository.ReadAllFlowsWithTheme();
    }
    
    public void SetParticipationByFlow(long flowId,string email)
    {
        _repository.AddParticipationByFlow(flowId,email);
    }

    public void ChangeFlowState(Flow flow)
    {
        _repository.UpdateFlowState(flow);
    }

    public StepBase AddStep(long flowId, int stepNumber, string stepType)
    {
        Flow flow = _repository.ReadFlowById(flowId);

        //InformationStep stepI = null;
        StepBase step = null;
        switch (stepType)
        {
            case "Information": step = new InformationStep(stepNumber, new Text(),flow);
                step.StepName = "Information"; break;
            case "Single Choice Question": 
                SingleChoiceQuestion singleChoiceQuestion = new SingleChoiceQuestion(); 
                step =  new QuestionStep(stepNumber,singleChoiceQuestion,flow);
                step.StepName = "Single Choice Question"; break;
            case "Multiple Choice Question":
                MultipleChoiceQuestion multipleChoiceQuestion = new MultipleChoiceQuestion(); 
                step =  new QuestionStep(stepNumber,multipleChoiceQuestion,flow); step.StepName = "Multiple Choice Question"; break;
            case "Ranged Question":
                RangeQuestion rangeQuestion = new RangeQuestion(); 
                step =  new QuestionStep(stepNumber,rangeQuestion,flow); step.StepName = "Ranged Question"; break;
            case "Open Question": 
                OpenQuestion openQuestion = new OpenQuestion(); 
                step =  new QuestionStep(stepNumber,openQuestion,flow); step.StepName = "Open Question"; break;
        }
        
        _repository.AddStepToFlow(flowId, step);

        return step;

    }

    public void DeleteFlowById(long flowId)
    {
        _repository.DeleteFlowById(flowId);
    }
    
    
    public string[] GetCountStepsPerFlow()
    {
        return _repository.GetCountStepsPerFlow();
    }
    public string[] GetCountParticipationsPerFlow()
    {
        return _repository.GetCountParticipationsPerFlow();
    }
    public string[] GetNamesPerFlow()
    {
        return _repository.GetNamesPerFlow();
    }
    public string[] GetQuestionCountsForFlow(string flowName)
    {
        return _repository.GetQuestionCountsForFlow(flowName);
    }
}