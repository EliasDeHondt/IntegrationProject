/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

namespace Domain.ProjectLogics.Steps.Questions;

public class Choice
{
    public long Id { get; set; }
    public string Text { get; set; }

    public Choice(string text, long id = 0)
    {
        Id = id;
        Text = text;
    }

    public Choice()
    {
        Id = default;
        Text = string.Empty;
    }
}