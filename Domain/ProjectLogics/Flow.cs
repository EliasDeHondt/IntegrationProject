using Domain.ProjectLogics.Steps;

namespace Domain.ProjectLogics;

public class Flow
{
    
    public FlowType FlowType { get; set; }
    
    public IEnumerable<ISteps> Steps { get; set; }
    
    public IEnumerable<Participation> Participations { get; set; }

    public void StartFlow()
    {
        throw new NotImplementedException();
    }

    public void StopFlow()
    {
        throw new NotImplementedException();
    }
    
    
}