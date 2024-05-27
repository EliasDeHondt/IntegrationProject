/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

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

    public IEnumerable<MainTheme> ReadAllMainThemes()
    {
        return _context.MainThemes
            .AsNoTracking()
            .ToList();
    }

    public MainTheme ReadMainThemeById(long id)
    {
        return _context.MainThemes
            .Single(theme => theme.Id == id);
    }


    public SubTheme ReadSubThemeByIdIncludingMainThemeAndProject(long id)
    {
        return _context.SubThemes
            .AsNoTracking()
            .Include(theme => theme.MainTheme)
            .Single(theme => theme.Id == id);
    }

    public IEnumerable<SubTheme> ReadSubThemesOfMainTheme(long id)
    {
        return _context.SubThemes
            .AsNoTracking()
            .Include(theme => theme.MainTheme)
            .Where(theme => theme.MainTheme.Id.Equals(id))
            .ToList();
    }

    public IEnumerable<Flow> ReadFlowsOfSubThemeById(long id)
    {
        return _context.Flows
            .AsNoTracking()
            .Include(flow => flow.Theme)
            .Where(flow => flow.Theme.Id.Equals(id))
            .ToList();
    }

    public SubTheme CreateSubTheme(SubTheme theme)
    {
        _context.SubThemes.Add(theme);
        return theme;
    }

    public IEnumerable<SubTheme> ReadSubthemesForProject(long id)
    {
        var subThemes = _context.SubThemes
            .AsNoTracking()
            .Include(theme => theme.MainTheme)
            .Where(theme => theme.MainTheme.Id == id)
            .ToList();
        
        return subThemes;
    }

    public void UpdateSubTheme(long id, string subject)
    {
        _context.SubThemes.Find(id)!.Subject = subject;
    }


    public Flow CreateFlowForSub(Flow flow)
    {
        _context.Flows.Add(flow);

        return flow;
    }
    public SubTheme ReadSubThemeById(long themeId)
    {
        return _context.SubThemes.Find(themeId)!;
    }

    public void DeleteSubTheme(long id)
    {
        SubTheme subTheme = _context.SubThemes.Find(id)!;
        
        _context.SubThemes.Remove(subTheme);

    }

    public IEnumerable<long> ReadSubThemeFlows(long id)
    {

        var flowIds = _context.SubThemes
            .AsNoTracking()
            .Where(theme => theme.Id == id)
            .SelectMany(theme => theme.Flows)
            .Select(flow => flow.Id)
            .ToList();
        
        return flowIds;
    }

    public long? ReadProjectId(long themeId)
    {
        SubTheme subTheme = _context.SubThemes.Find(themeId)!;

        foreach (ThemeBase themeBase in _context.ThemeBases)
        {
            //I don't know why, but if this foreach isn't here nothing works. Wonky but works.
        }
        
        foreach (Project project in _context.Projects)
        {
            foreach (SubTheme theme in project.MainTheme.Themes)
            {
                if (theme.Id == subTheme.Id)
                {
                    return project.Id;
                }
            }
        }

        return null;
    }
}