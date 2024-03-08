using Domain.ProjectLogics.Steps;

namespace Domain.FacilitatorFunctionality;

public class Note
{
    public IStep Step { get; set; }
    public long Id { get; set; }
    public String Textfield { get; set; }
}