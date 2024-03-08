namespace Domain.ProjectLogics;

public interface ITheme
{
    public long Id { get; set; }
    public IEnumerable<Flow> Flows { get; set; }
}