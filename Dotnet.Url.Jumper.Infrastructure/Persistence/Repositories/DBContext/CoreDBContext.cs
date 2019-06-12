using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext
{
    public class CoreDbContext : DbContext
    {
        private readonly string _connectionString = string.Empty;

        public CoreDbContext(IConfiguration configuration) : base()
        {
            _connectionString = configuration.GetConnectionString("CoreConnectionString");            
            this.Database.EnsureCreated();            
        }

        public DbSet<DbStat> Stats { get; set; }
        public DbSet<DBAdmin> Admins { get; set; }
        public DbSet<DbVisitor> Visitors { get; set; }
        public DbSet<DbShortUrl> ShortUrls { get; set; }

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
            base.OnModelCreating(modelBuilder);
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
