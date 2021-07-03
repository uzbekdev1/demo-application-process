using Demo.ApplicationProcess.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.ApplicationProcess.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
         
        public DbSet<NewsEntity> News { get; set; }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             
            modelBuilder.Entity<NewsEntity>(entity =>
            {

                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);

            }); 

        }

    }
}
