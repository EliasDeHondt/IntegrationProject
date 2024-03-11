/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

namespace Domain.WebApp;

public class Idea
{
    public long Id { get; set; }
    public String Text { get; set; }
    public List<Reaction> Reactions { get; set; }
    public List<Like> Likes { get; set;  }
}