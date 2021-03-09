using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Repositories
{
    public interface IReadRepository<T, TType>
    {
        IEnumerable<T> List();
        T FindById(TType id);
        T FindByCreationDate(DateTime creationDate);
        T FindByModificationDate(DateTime modificationDate);
        IEnumerable<T> FindBetween(DateTime from, DateTime to);
    }
}
