﻿using DevFreela.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Persistence
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

            builder.Entity<UserSkill>(s =>
            {
                s.HasKey(s => s.Skill);
                s.HasOne(s => s.Skill)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(s => s.IdSkill).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ProjectComment>(e =>
            {
                e.HasKey(p => p.Project);
                e.HasOne(p => p.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.IdProject)
                .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);
                e.HasMany(u => u.Skills)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.IdUser)
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
        public DbSet<Project> Projects { get; private set; }
        public DbSet<Project> Users { get; private set; }
        public DbSet<Project> Skills { get; private set; }
        public DbSet<UserSkill> UserSkills { get; private set; }
        public DbSet<ProjectComment> ProjectComments { get; private set; }
    }
}
