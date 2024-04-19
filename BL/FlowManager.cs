/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;

namespace Business_Layer;

public class FlowManager
{
    private readonly FlowRepository _repository;

    public FlowManager(FlowRepository repository)
    {
        _repository = repository;
    }

    public Flow Add(Flow item)
    {
        throw new NotImplementedException();
    }

    public Flow Update(Flow item)
    {
        throw new NotImplementedException();
    }

    public Flow Remove(Flow item)
    {
        throw new NotImplementedException();
    }

    public Flow GetFlowById(long id)
    {
        return _repository.ReadFlowById(id);
    }

    public Flow GetFlowByIdWithTheme(long id)
    {
        return _repository.ReadFlowByIdIncludingTheme(id);
    }
    
    public List<Flow> GetAll()
    {
        throw new NotImplementedException();
    }

    public void ChangeFlowState(Flow flow)
    {
        _repository.UpdateFlowState(flow);
    }
}