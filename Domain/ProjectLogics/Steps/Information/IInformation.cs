namespace Domain.ProjectLogics.Steps.Information;

public interface IInformation
{

    IStep Step { get; set; }
    string GetInformation();
    
}