using Data_Access_Layer.DbContext;
using Domain.Accounts;
using Domain.ProjectLogics;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer;

public class ProjectRepository
{

    private readonly CodeForgeDbContext _ctx;
    

    public ProjectRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }

    public IEnumerable<Project> ReadProjectsFromIds(IEnumerable<long> ids)
    {
        return _ctx.Projects.Where(project => ids.Contains(project.Id));
    }

    public void AddFacilitatorToProjects(Facilitator facilitator, params long[] projectIds)
    {
        _ctx.Projects.Where(project => projectIds.Contains(project.Id)).ToList().ForEach(project => project.Facilitators.Add(facilitator));
    }

}