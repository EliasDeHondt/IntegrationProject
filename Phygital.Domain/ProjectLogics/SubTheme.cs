/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

namespace Domain.ProjectLogics;

public class SubTheme : ThemeBase
{
    public MainTheme MainTheme { get; set; }

    public SubTheme(string subject, ICollection<Flow> flows, MainTheme mainTheme, long id = 0) : base(subject, flows, id)
    {
        MainTheme = mainTheme;
    }

    public SubTheme(string subject, MainTheme mainTheme, long id = 0) : base(subject, id)
    {
        MainTheme = mainTheme;
    }
    
    public SubTheme()
    {
        MainTheme = new MainTheme();
    }
}