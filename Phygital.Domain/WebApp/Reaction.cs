/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.Accounts;

namespace Domain.WebApp;

public class Reaction
{
    public long ReactionId { get; set; }
    public String Text { get; set; }
    public Idea Idea { get; set; }
    public User User { get; set; }
}