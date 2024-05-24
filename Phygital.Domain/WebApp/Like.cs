/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.Accounts;

namespace Domain.WebApp;

public class Like
{
    public Idea Idea { get; set; }
    public WebAppUser WebAppUser { get; set; }

    public Like(Idea idea, WebAppUser webAppUser)
    {
        Idea = idea;
        WebAppUser = webAppUser;
    }
    
    public Like()
    {
        Idea = new Idea();
        WebAppUser = new WebAppUser();
    }
    
}