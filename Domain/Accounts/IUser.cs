namespace Domain.Accounts;

public interface IUser
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    void Login()
    {
    }
}