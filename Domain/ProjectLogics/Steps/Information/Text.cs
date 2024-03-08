namespace Domain.ProjectLogics.Steps.Information;

public class Text : IInformation
{
    public string InformationText { get; set; }
    public IStep Step { get; set; }
    
    public Text(string informationText)
    {
        InformationText = informationText;
    }

    public string GetInformation()
    {
        return InformationText;
    }
}