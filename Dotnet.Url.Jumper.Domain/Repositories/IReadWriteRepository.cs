namespace Dotnet.Url.Jumper.Domain.Repositories
{
    public interface IReadWriteRepository<T, TType> : IReadRepository<T, TType>, IWriteRepository<T, TType>
    {
    }
}
