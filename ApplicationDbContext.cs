using api_app.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Emit;

namespace api_app
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("AspNetUsers");
            builder.Entity<Assigned_Activity>().HasKey(x => new { x.UserId, x.Developed_ActivityId });

        }


        public DbSet<Project> Projects { get; set; }
        public DbSet<Responsability> Responsabilities { get; set; }
        public DbSet<Job> Jobs { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity_next_day> Activities_next_day { get; set; }
      
        public DbSet<Developed_Activity> Developed_activities { get; set; }
        public DbSet<Need_next_day> Needs_next_day { get; set; }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Observation> Observations { get; set; }
        public DbSet<Assigned_Activity> Assigned_Activities { get; set; }


    }
}
