using Domain.ProjectLogics.Steps;

namespace Domain.ProjectLogics;

public class Flow
{
    public long Id { get; set; }
    public FlowType FlowType { get; set; }
    public ITheme Theme { get; set; }
    public IEnumerable<IStep> Steps { get; set; }
    public IEnumerable<Participation> Participations { get; set; }
    
    private bool FlowStopped { get; set; }

    public Flow(FlowType flowTye, IEnumerable<IStep> steps, IEnumerable<Participation> participations)
    {
        FlowType = flowTye;
        Steps = steps;
        Participations = participations;
    }
    
    public Flow(FlowType flowType)
    {
        FlowType = flowType;
        Steps = new List<IStep>();
        Participations = new List<Participation>();
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
    
    
}