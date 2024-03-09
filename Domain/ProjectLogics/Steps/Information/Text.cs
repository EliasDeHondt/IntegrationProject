using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectLogics.Steps.Information;

public class Text : IInformation
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(600)]
    public string InformationText { get; set; }
    
    public Text(string informationText, long id = 0)
    {
        Id = id;
        InformationText = informationText;
    }

    public Text()
    {
        Id = default;
        InformationText = string.Empty;
    }

    public string GetInformation()
    {
        return InformationText;
    }
}