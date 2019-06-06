using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.DBContext;
using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext
{
    public class CoreDbContext<T> : DbContext, IRepoContext<T> where T : class
    {
        private readonly string _connectionString = string.Empty;

        public CoreDbContext(IConfiguration configuration) : base()
        {
            this._connectionString = configuration.GetConnectionString("CoreConnectionString");
        }

        private DbSet<T> _dbSet => this.Set<T>();
        public IQueryable<T> Entity => _dbSet;

        public void CloseConnection()
        {
            if (this.Database.GetDbConnection().State != System.Data.ConnectionState.Closed)
            {
                this.Database.CloseConnection();
            }
        }

        public void OpenConnection()
        {
            if (this.Database.GetDbConnection().State != System.Data.ConnectionState.Open)
            {
                this.Database.OpenConnection();
            }
        }

        public void Add(T entity)
        {
            this.Add(entity);            
        }

        public void AddRange(IEnumerable<T> entities)
        {
            this.AddRange(entities);
        }

        public void Delete(T entity)
        {
            this.Delete(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            this.DeleteRange(entities);
        }

        public override int SaveChanges()
        {
            var AddedEntities = ChangeTracker.Entries<CoreDbEntity>().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Entity.AddedDate = DateTime.Now;
            });

            var ModifiedEntities = ChangeTracker.Entries<CoreDbEntity>().Where(E => E.State == EntityState.Modified).ToList();

            ModifiedEntities.ForEach(E =>
            {
                E.Entity.ModifiedDate = DateTime.Now;
            });

            return base.SaveChanges();
        }
    }
}
