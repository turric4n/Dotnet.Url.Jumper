using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Repositories
{
    public interface IWriteRepository<T, TType>
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(TType id);
    }
}
