/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer;
using Domain.Accounts;
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

    public void AddFacilitatorToProjects(Facilitator facilitator,params long[] projectId)
    {
        _repo.AddFacilitatorToProjects(facilitator, projectId);
    }

}