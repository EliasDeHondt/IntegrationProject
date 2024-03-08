using Domain.Accounts;

namespace Domain.WebApp;

public class Reaction
{
    public String Text { get; set; }
    public Idea Idea { get; set; }
    public User User { get; set; }
}