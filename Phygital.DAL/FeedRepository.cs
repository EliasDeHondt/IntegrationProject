using Data_Access_Layer.DbContext;

namespace Data_Access_Layer;

public class FeedRepository
{
    
    public CodeForgeDbContext _ctx { get; set; }

    public FeedRepository(CodeForgeDbContext ctx)
    {
        _ctx = ctx;
    }
    
}