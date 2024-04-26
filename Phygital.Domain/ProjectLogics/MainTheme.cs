/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

namespace Domain.ProjectLogics;

public class MainTheme : ThemeBase
{ public ICollection<SubTheme> Themes { get; set; }
    
    public MainTheme(string subject, ICollection<Flow> flows, ICollection<SubTheme> themes, long id = 0) : base(subject, flows, id)
    {
        Themes = themes;
    }
    
    public MainTheme(string subject, long id = 0) : base(subject, id)
    {
        Themes = new List<SubTheme>();
    }

    public MainTheme()
    {
        Themes = new List<SubTheme>();
    }
}