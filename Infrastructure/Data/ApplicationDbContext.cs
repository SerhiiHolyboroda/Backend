
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace Abackend.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions options) : base(options) {
        }

       public  DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<ProductType> ProductTypes{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //      base.OnModelCreating(modelBuilder);
            //      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

/*
      if (Database.ProviderName == "Microsoft.Entity Framework.Sqlite")
      {
      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
      var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType ==typeof(decimal));
      foreach (var property in properties)
      {
      modelBuilder.Entity(entityType.Name).Property(property.Name)
      .HasConversion<double>();
      }
           */ 
        }
    }
}
