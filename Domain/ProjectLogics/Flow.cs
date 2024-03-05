using Domain.ProjectLogics.Steps;

namespace Domain.ProjectLogics;

public class Flow
{
    
    public FlowType FlowType { get; set; }
    
    public IEnumerable<ISteps> Steps { get; set; }
    
    public IEnumerable<Participation> Participations { get; set; }
    
    private bool FlowStopped { get; set; }

    public Flow(FlowType flowType)
    {
        FlowType = flowType;
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