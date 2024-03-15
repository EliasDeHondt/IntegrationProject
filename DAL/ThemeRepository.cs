/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer.DbContext;
using Domain.ProjectLogics;

namespace Data_Access_Layer;

public class ThemeRepository
{
    private readonly CodeForgeDbContext _context;

    public ThemeRepository(CodeForgeDbContext context)
    {
        _context = context;
    }

    public ThemeBase Read(long id, Type type)
    {
        if (type == typeof(MainTheme))
        {
            return _context.MainThemes.Find(id);
        }

        if (type == typeof(SubTheme))
        {
            return _context.SubThemes.Find(id);
        }

        return null;
    }

    public IEnumerable<MainTheme> ReadAllMainThemes()
    {
        return _context.MainThemes;
    }

    public MainTheme ReadMainThemeById(long id)
    {
        return _context.MainThemes.Find(id);
    }

    public IEnumerable<SubTheme> ReadSubThemesOfMainThemeById(long id)
    {
        MainTheme theme = _context.MainThemes.Find(id);
        _context.Entry(theme).Collection(theme => theme.Themes).Load();
        return theme.Themes;
    }

    public IEnumerable<Flow> ReadFlowsOfMainThemeById(long id)
    {
        MainTheme theme = _context.MainThemes.Find(id);
        _context.Entry(theme).Collection(theme => theme.Flows).Load();
        return theme.Flows;
    }
}