/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.ProjectLogics.Steps;
using FileHelpers;

namespace Domain.ProjectLogics;

public class Flow
{
    public long Id { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FlowType FlowType { get; set; }
    public ICollection<StepBase> Steps { get; set; }
    public ICollection<Participation> Participations { get; set; }
    public ThemeBase Theme { get; set; }
    private bool FlowStopped { get; set; }
    public FlowState State { get; set; }

    public Flow(FlowType flowType, ICollection<StepBase> steps, ICollection<Participation> participations, ThemeBase theme, long id = 0)
    {
        Id = id;
        FlowType = flowType;
        Steps = steps;
        Participations = participations;
        Theme = theme;
        State = FlowState.Inactive;
    }
    
    public Flow(FlowType flowType, ThemeBase theme, long id = 0)
    {
        Id = id;
        FlowType = flowType;
        Steps = new List<StepBase>();
        Participations = new List<Participation>();
        Theme = theme;
        State = FlowState.Inactive;
    }

    public Flow()
    {
        Id = default;
        FlowType = FlowType.Unknown;
        Steps = new List<StepBase>();
        Participations = new List<Participation>();
        Theme = new MainTheme();
        State = FlowState.Inactive;
    }

    public void StartFlow()
    {
        PlayFlow();
        if (FlowType == FlowType.Circular && !FlowStopped)
        {
            StartFlow();
        }

        FlowStopped = false;
    }

    private void PlayFlow()
    {
        using var stepEnumerator = Steps.GetEnumerator();
        while (stepEnumerator.MoveNext() && !FlowStopped)
        {
            //TODO
        }
    }
    
    public void StopFlow()
    {
        FlowStopped = true;
    }

    public int getStepCount()
    {
        //return Steps.Count; //todo --> dit in de step.cshtml doen
        int stepsCount = 0;
        foreach (var step in Steps)
        {
            stepsCount++;
        }
        
        return stepsCount;
    }
}