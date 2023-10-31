using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using BlazorOdata.Shared;
using Microsoft.Data.Edm;

namespace BlazorOdata.Server.EfCore
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Template> Templates => Set<Template>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Template>(entity =>
            {
                entity.ToTable("Template");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);
            });
        }
    }

    public static class CustomEdmModel
    {
        public static Microsoft.OData.Edm.IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<Template>("Template");

            return builder.GetEdmModel();
        }

    }
}
