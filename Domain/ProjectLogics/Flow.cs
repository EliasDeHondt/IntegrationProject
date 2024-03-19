/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.ProjectLogics.Steps;

namespace Domain.ProjectLogics;

public class Flow
{
    [Key]
    public long Id { get; set; }
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FlowType FlowType { get; set; }
    [Required]
    public ICollection<StepBase> Steps { get; set; }
    [Required]
    public ICollection<Participation> Participations { get; set; }
    [Required]
    public ThemeBase Theme { get; set; }
    
    private bool FlowStopped { get; set; }

    public Flow(FlowType flowTye, ICollection<StepBase> steps, ICollection<Participation> participations, ThemeBase theme, long id = 0)
    {
        Id = id;
        FlowType = flowTye;
        Steps = steps;
        Participations = participations;
        Theme = theme;
    }
    
    public Flow(FlowType flowType, ThemeBase theme, long id = 0)
    {
        Id = id;
        FlowType = flowType;
        Steps = new List<StepBase>();
        Participations = new List<Participation>();
        Theme = theme;
    }

    public Flow()
    {
        Id = default;
        FlowType = FlowType.UNKNOWN;
        Steps = new List<StepBase>();
        Participations = new List<Participation>();
        Theme = new MainTheme();
    }

    public void StartFlow()
    {
        PlayFlow();
        if (FlowType == FlowType.CIRCULAR && !FlowStopped)
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