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

    public IEnumerable<SubTheme> GetSubThemesOfMainThemeById(long id)
    {
        return _repository.ReadSubThemesOfMainThemeById(id);
    }
    
    public IEnumerable<Flow> GetFlowsOfMainThemeById(long id)
    {
        return _repository.ReadFlowsOfMainThemeById(id);
    }
}