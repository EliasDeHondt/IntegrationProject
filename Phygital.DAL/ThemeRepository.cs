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
            .First(theme => theme.Id == id);
    }


    public SubTheme ReadSubThemeByIdIncludingMainThemeAndProject(long id)
    {
        return _context.SubThemes
            .AsNoTracking()
            .Include(theme => theme.MainTheme)
            .First(theme => theme.Id == id);
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
        var mainTheme = _context.MainThemes.Find(id);
        var subThemes = _context.SubThemes
            .AsNoTracking()
            .Include(theme => theme.MainTheme)
            .Where(theme => theme.MainTheme.Id == mainTheme!.Id)
            .ToList();
        return subThemes;
    }

    public void UpdateSubTheme(long id, string subject)
    {
        _context.SubThemes.Find(id)!.Subject = subject;
    }


    public Flow CreateFlowForSub(FlowType type, long themeId)
    {
        var theme = _context.SubThemes.Find(themeId);
        var flow = new Flow(type, theme);
        _context.Flows.Add(flow);

        return flow;
    }

    public void DeleteSubTheme(long id)
    {
        SubTheme subTheme = _context.SubThemes.Find(id)!;
        
        _context.SubThemes.Remove(subTheme);

    }

    public IEnumerable<long> GetSubThemeFlows(long id)
    {
        IEnumerable<long> flowIds = Array.Empty<long>();
        
        SubTheme subTheme = _context.SubThemes.Find(id)!;
        
        IEnumerable<Flow> flows = subTheme.Flows;

        foreach (Flow flow in flows)
        {
            flowIds = flowIds.Append(flow.Id);
        }

        return flowIds;
    }
}