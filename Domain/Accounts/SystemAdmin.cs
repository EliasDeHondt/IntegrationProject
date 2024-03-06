namespace Domain.Accounts;

public class SystemAdmin: IUser
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
}