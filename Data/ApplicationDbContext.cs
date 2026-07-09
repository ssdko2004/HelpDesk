using HelpDesk.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace HelpDesk.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : 
        IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Project> Projects => Set<Project>();  
        public DbSet<TicketCategory> TicketCategories => Set<TicketCategory>();
        public DbSet<Ticket> Tickets => Set<Ticket>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Department>(entity =>
            {
                entity.HasIndex(d => d.Name).IsUnique();
            });

            builder.Entity<Project>(entity =>
            {
                entity.HasIndex(p => p.Code).IsUnique();
                entity.HasIndex(p => p.Name).IsUnique();

                entity.HasOne(p => p.Department)
                    .WithMany()
                    .HasForeignKey(p => p.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.TeamLeadUser)
                    .WithMany()
                    .HasForeignKey(p => p.TeamLeadUserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<TicketCategory>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
            });

            builder.Entity<Ticket>(entity =>
            {
                entity.HasOne(t => t.Project)
                    .WithMany()
                    .HasForeignKey(t => t.ProjectId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(t => t.Department)
                    .WithMany()
                    .HasForeignKey(t => t.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(t => t.Category)
                    .WithMany()
                    .HasForeignKey(t => t.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.RequesterUser)
                    .WithMany()
                    .HasForeignKey(t => t.RequesterUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.AssignedToUser)
                    .WithMany()
                    .HasForeignKey(t => t.AssignedToUserId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(t => t.AssignedByUser)
                    .WithMany()
                    .HasForeignKey(t => t.AssignedByUserId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(t => new { t.Status, t.AssignedToUserId });
                entity.HasIndex(t => new { t.ProjectId, t.Status });
                entity.HasIndex(t => new { t.RequesterUserId, t.CreatedAtUtc });
            });
        }
    }
}