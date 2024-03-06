using Domain.Accounts;

namespace Domain.WebApp;

public class Like
{
    public Idea Idea { get; set; }
    public IUser User { get; set; }
}