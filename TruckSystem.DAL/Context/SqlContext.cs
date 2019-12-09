using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TruckSystem.Domain.Vehicles.Model;

namespace TruckSystem.DAL.Context
{
    [ExcludeFromCodeCoverage]
    public class SqlContext : DbContext
    {
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Model> Models { get; set; }

        public SqlContext(DbContextOptions<SqlContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configurationTypes = GetType().Assembly
                    .GetTypes()
                    .Where(x =>
                        x.GetTypeInfo().IsClass
                        && !x.GetTypeInfo().IsAbstract
                        && x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            var configurations = configurationTypes.Select(Activator.CreateInstance);

            foreach (dynamic configuration in configurations)
            {
                modelBuilder.ApplyConfiguration(configuration);
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            DetectDataChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            DetectDataChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            DetectDataChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            DetectDataChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void DetectDataChanges()
        {
            ChangeTracker.DetectChanges();
            var entries = ChangeTracker.Entries()?.Where(x => x.State == EntityState.Added || x.State == EntityState.Modified).ToList() ?? new List<EntityEntry>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Properties.Any(x => x.Metadata.Name == "DateCreated"))
                    {
                        entry.Property("DateCreated").CurrentValue = DateTime.Now;
                    }
                }
            }
        }
    }  
}
