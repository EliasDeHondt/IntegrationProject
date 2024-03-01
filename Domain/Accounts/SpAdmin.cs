namespace Domain.Accounts;

public class SpAdmin: IUser
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public long SpAdminId { get; set; }
}