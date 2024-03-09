namespace Domain.ProjectLogics;

public interface ITheme
{
    public long Id { get; set; }
    public string Subject { get; set; }
    public ICollection<Flow> Flows { get; set; }
}