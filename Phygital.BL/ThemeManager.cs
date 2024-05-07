/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer;
using Domain.ProjectLogics;

namespace Business_Layer;

public class ThemeManager
{
    private readonly ThemeRepository _repository;
    
    public ThemeManager(ThemeRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<MainTheme> GetAllMainThemes()
    {
        return _repository.ReadAllMainThemes();
    }

    public MainTheme GetMainThemeById(long id)
    {
        return _repository.ReadMainThemeById(id);
    }

    public SubTheme GetSubThemeByIdWithMainThemeAndProject(long id)
    {
        return _repository.ReadSubThemeByIdIncludingMainThemeAndProject(id);
    }

    public IEnumerable<SubTheme> GetSubThemesOfMainThemeById(long id)
    {
        return _repository.ReadSubThemesOfMainTheme(id);
    }

    public IEnumerable<Flow> GetFlowsOfSubThemeById(long id)
    {
        return _repository.ReadFlowsOfSubThemeById(id);
    }

    public SubTheme AddSubTheme(string subject, long mainThemeId)
    {
        var mainTheme = GetMainThemeById(mainThemeId);
        var theme = new SubTheme(subject, mainTheme);
        return _repository.CreateSubTheme(theme);
    }

    public IEnumerable<SubTheme> GetSubthemesForProject(long id)
    {
        return _repository.ReadSubthemesForProject(id);
    }


    public void UpdateSubTheme(long id, string subject)
    {
        _repository.UpdateSubTheme(id, subject);
    }

    public void DeleteSubTheme(long id)
    {
        _repository.DeleteSubTheme(id);
    }
}