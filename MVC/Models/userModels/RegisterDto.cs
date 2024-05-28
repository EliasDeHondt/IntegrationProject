using System.ComponentModel.DataAnnotations;

namespace MVC.Models.userModels;

public class RegisterDto
{
    [MinLength(5)]
    [MaxLength(50)]
    public string Name { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [MinLength(10)]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{10,}$")]
    public string Password { get; set; }
    public long FeedId { get; set; }

}