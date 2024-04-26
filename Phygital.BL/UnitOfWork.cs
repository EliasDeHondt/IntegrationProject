/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer.DbContext;

namespace Business_Layer;

public class UnitOfWork
{
    private readonly CodeForgeDbContext _dbContext;

    public UnitOfWork(CodeForgeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void BeginTransaction()
    {
        _dbContext.Database.BeginTransaction();
    }

    public void Commit()
    {
        _dbContext.SaveChanges();
        _dbContext.Database.CommitTransaction();
    }
}