
using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext : DbContext
    {
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Skill>(e =>
            {
                e.HasKey(s => s.Id);
            });

            builder.Entity<UserSkill>(US =>
            {
                US.HasKey(us => us.Id);

                US.HasOne(us => us.Skill)
                    .WithMany(s=> s.UserSkills)
                    .HasForeignKey(us => us.IdSkill)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ProjectComment>(e =>
            {
                e.HasKey(p => p.Id);
             
                e.HasOne(p => p.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.IdProject)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.User)
                .WithMany(u=>u.Comments)
                .HasForeignKey(p=>p.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);

                e.HasMany(u => u.Skills)
                    .WithOne(us => us.User)
                    .HasForeignKey(us => us.IdUser)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Project>(e =>
            {
                e.HasKey(p => p.Id);

                e.HasOne(p => p.Freelancer)
                    .WithMany(f => f.FreelancerProjects)
                    .HasForeignKey(p => p.IdFreelancer)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.Client)
                    .WithMany(c => c.OwnedProjects)
                    .HasForeignKey(p => p.IdClient)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(builder);
        }
        public DbSet<User> Users { get; private set; }
        public DbSet<Skill> Skills { get; private set; }
        public DbSet<Project> Projects { get; private set; }
        public DbSet<UserSkill> UserSkills { get; private set; }
        public DbSet<ProjectComment> ProjectComments { get; private set; }
    }
}
