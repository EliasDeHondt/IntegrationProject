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

public class FlowRepository
{
    private readonly CodeForgeDbContext _context;

    public FlowRepository(CodeForgeDbContext context)
    {
        _context = context;
    }

    public Flow ReadFlowById(long id)
    {
        return _context.Flows.Find(id);
    }
    
    public Flow ReadFlowByIdIncludingTheme(long id)
    {
        return _context.Flows
            .Include(f => f.Theme)
            .Include(f => f.Steps)
            .FirstOrDefault(f => f.Id == id);
    }

    public void UpdateFlowState(Flow flow)
    {
        _context.Flows.Update(flow);
        _context.SaveChanges();
    }
}