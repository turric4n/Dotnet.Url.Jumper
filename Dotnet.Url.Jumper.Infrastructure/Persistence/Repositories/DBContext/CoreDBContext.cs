using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.DBContext;
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
            _connectionString = configuration.GetConnectionString("CoreConnectionString");            
            this.Database.EnsureCreated();
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>();
            base.OnModelCreating(modelBuilder);
        }

        public T Add(T entity)
        {
            try
            {
                var ent = _dbSet.AddAsync(entity).Result.Entity;
                SaveChanges();
                Entry(ent).State = EntityState.Detached;
                return ent;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public T Update(T entity)
        {
            try
            {                               
                var ent = _dbSet.Update(entity);
                SaveChanges();              
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            SaveChanges();
        }

        public override int SaveChanges()
        {
            var AddedEntities = ChangeTracker.Entries<CoreDbEntity>().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Entity.AddedDate = DateTime.Now;
                E.Entity.ModifiedDate = DateTime.Now;
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
