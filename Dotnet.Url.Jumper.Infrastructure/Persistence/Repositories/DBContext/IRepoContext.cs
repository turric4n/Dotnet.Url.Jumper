using System.Collections.Generic;
using System.Linq;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.DBContext
{
    public interface IRepoContext<T> where T : class
    {
        IQueryable<T> Entity { get; }
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void OpenConnection();
        void CloseConnection();
    }
}
