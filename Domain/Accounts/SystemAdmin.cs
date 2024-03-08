namespace Domain.Accounts;

public class SystemAdmin: User
{
    public override long Id { get; set; }
    public override string Name { get; set; }
    public override string Email { get; set; }
    public override string Password { get; set; }
}