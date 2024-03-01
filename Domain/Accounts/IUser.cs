namespace Domain.Accounts;

public interface IUser
{
    public String Name { get; set; }
    public String Email { get; set; }
    public String Password { get; set; }

    void Login()
    {
    }
}