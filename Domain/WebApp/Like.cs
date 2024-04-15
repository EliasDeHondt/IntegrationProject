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
    public Respondent User { get; set; }
}