
namespace MVC.Models.projectModels;

public class ProjectDto
{
    public long MainThemeId { get; set; }
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
    public long SharedPlatformId { get; set; }
}