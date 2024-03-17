/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.Collections;
using Data_Access_Layer.DbContext;
using Domain.ProjectLogics;
using Microsoft.EntityFrameworkCore;

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


    public SubTheme ReadSubThemeById(long id)
    {
        return _context.SubThemes.Find(id);
    }

    public IEnumerable<SubTheme> ReadSubThemesOfMainTheme(long id)
    {
        return _context.SubThemes
            .Include(theme => theme.MainTheme)
            .Where(theme => theme.MainTheme.Id.Equals(id));
    }

    public IEnumerable<Flow> ReadFlowsOfMainThemeById(long id)
    {
        MainTheme theme = _context.MainThemes.Find(id);
        _context.Entry(theme).Collection(theme => theme.Flows).Load();
        return theme.Flows;
    }

    public IEnumerable<Flow> ReadFlowsOfSubThemeById(long id)
    {
        SubTheme theme = _context.SubThemes.Find(id);
        _context.Entry(theme).Collection(theme => theme.Flows).Load();
        return theme.Flows;
    }
}