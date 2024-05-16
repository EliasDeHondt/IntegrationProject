/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

namespace Domain.WebApp;

public class Idea
{
    public long IdeaId { get; set; }
    public string Text { get; set; }
    public List<Reaction> Reactions { get; set; }
    public List<Like> Likes { get; set;  }
}