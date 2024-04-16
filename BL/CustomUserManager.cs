using Data_Access_Layer;
using Domain.Accounts;

namespace Business_Layer;

public class CustomUserManager
{
    private readonly UserRepository _repo;

    public CustomUserManager(UserRepository repo)
    {
        _repo = repo;
    }

    public SpAdmin GetPlatformAdminWithSharedPlatform(string email)
    {
        return _repo.ReadPlatformAdminIncludingSharedPlatform(email);
    } 
    
}