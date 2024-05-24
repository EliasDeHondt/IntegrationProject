namespace MVC.Models.platformModels;

public class CreatePlatformDto
{
    
    public long Id { get; set; }
    public string OrganisationName { get; set; }
    public string Logo { get; set; }
    
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
}