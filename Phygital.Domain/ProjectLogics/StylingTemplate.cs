namespace Domain.ProjectLogics;

public class StylingTemplate
{
    public long Id { get; set; }
    public long ProjectId { get; set; }
    public string ThemeName { get; set; }
    public string CustomPrimaryColor { get; set; }
    public string CustomSecondaryColor { get; set; }
    public string CustomBackgroundColor { get; set; }
    public string CustomAccentColor { get; set; }

    public StylingTemplate(long projectId, string themeName="Light", string customPrimaryColor="#479ecd", string customSecondaryColor="#e9521c", string customBackgroundColor="#f4edd2", string customAccentColor="#f57e00")
    {
        Id = default;
        ProjectId = projectId;
        ThemeName = themeName;
        CustomPrimaryColor = customPrimaryColor;
        CustomSecondaryColor = customSecondaryColor;
        CustomBackgroundColor = customBackgroundColor;
        CustomAccentColor = customAccentColor;
    }
}