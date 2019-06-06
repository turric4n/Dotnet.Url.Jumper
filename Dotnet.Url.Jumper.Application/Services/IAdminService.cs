using Dotnet.Url.Jumper.Application.Models;
using System.Collections.Generic;

namespace Dotnet.Url.Jumper.Application.Services
{
    public interface IAdminService
    {
        Admin Authenticate(string username, string password);
        Admin GetByUsername(string username);
        Admin GetById(int id);
    }
}
