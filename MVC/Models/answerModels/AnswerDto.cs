/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

namespace MVC.Models;

public class AnswerDto
{
    public ICollection<string> Answers { get; set; }
    public string AnswerText { get; set; } = null;
}