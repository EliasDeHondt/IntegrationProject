/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer;
using Domain.Accounts;
using Domain.FacilitatorFunctionality;
using Domain.Platform;
using Domain.ProjectLogics;

namespace Business_Layer;

public class ProjectManager
{

    public readonly ProjectRepository _repo;
    

    public ProjectManager(ProjectRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Project> GetAllProjectsFromIds(IEnumerable<long> ids)
    {
        return _repo.ReadProjectsFromIds(ids);
    }

    public Project GetProject(long id)
    {
        return _repo.ReadProject(id);
    }

    public IEnumerable<Project> GetAllProjectsForSharedPlatformWithMainTheme(long platformId)
    {
        return _repo.ReadAllProjectsForSharedPlatformIncludingMainTheme(platformId);
    }
    
    public void AddProjectOrganizer(Facilitator facilitator, Project project)
    {
        var projectOrganizer = new ProjectOrganizer
        {
            Facilitator = facilitator,
            Project = project
        };
        _repo.CreateProjectOrganizer(projectOrganizer);
    }

    public void AddProject(MainTheme mainTheme,SharedPlatform sharedPlatform,long id)
    {
        _repo.CreateProject(mainTheme,sharedPlatform,id);
    }
}