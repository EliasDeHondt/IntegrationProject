namespace Domain.Accounts;

public class SystemAdmin: IUser
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public long AdminId { get; set; }
}