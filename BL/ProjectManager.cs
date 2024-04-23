/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer;
using Domain.Accounts;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Identity;

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

    public IEnumerable<Project> GetPossibleProjectsForFacilitator(string email)
    {
        return _repo.ReadPossibleProjectsForFacilitator(email);
    }

    public IEnumerable<Project> GetAssignedProjectsForFacilitator(string email)
    {
        return _repo.ReadAssignedProjectsForFacilitator(email);
    }

    public void DeleteProjectOrganizer(Facilitator user, Project project)
    {
        _repo.RemoveProjectOrganizer(user, project);
    }
}