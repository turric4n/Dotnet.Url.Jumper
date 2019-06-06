using Dotnet.Url.Jumper.Domain.Models;

namespace Dotnet.Url.Jumper.Domain.Repositories
{
    public interface IAdminRepository : IReadWriteRepository<Admin, int>
    {
        Admin FindByUsername(string userName);
    }
}
