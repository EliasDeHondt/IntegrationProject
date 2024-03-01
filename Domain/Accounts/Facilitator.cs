namespace Domain.Accounts;

public class Facilitator: IUser
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public long FacilitatorId { get; set; }
}