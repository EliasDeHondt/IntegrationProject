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

    public SubTheme GetSubThemeById(long id)
    {
        return _repository.ReadSubThemeById(id);
    }

    public IEnumerable<SubTheme> GetSubThemesOfMainThemeById(long id)
    {
        return _repository.ReadSubThemesOfMainTheme(id);
    }

    public IEnumerable<Flow> GetFlowsOfSubThemeById(long id)
    {
        return _repository.ReadFlowsOfSubThemeById(id);
    }
}