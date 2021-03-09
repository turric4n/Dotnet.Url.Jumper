using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Dotnet.Url.Jumper.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Linq;

namespace Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext
{
    public class CoreDbContext : DbContext
    {
        private readonly string _connectionString = string.Empty;
        private readonly IOptions<InfrastructureSettings> _settings;
        private readonly ILogger<CoreDbContext> _logger;

        public CoreDbContext(IConfiguration configuration, IOptions<InfrastructureSettings> settings, 
            ILogger<CoreDbContext> logger) : base()
        {
            _connectionString = configuration.GetConnectionString("CoreConnection");
            _settings = settings;
            _logger = logger;
            Database.EnsureCreated();     
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
            if (_settings.Value.databaseEngine == "EntityFrameworkSQLite")
            {
                optionsBuilder.UseSqlite(_connectionString);                
            }
            else
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<DbStat>().Property(e => e.shortUrl).ValueGeneratedNever();
            //modelBuilder.Entity<DbStat>().Property(e => e.visitor).ValueGeneratedNever();
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
