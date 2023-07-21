using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class MyProjectDbContext : IdentityDbContext<UserModel>
    {

        public MyProjectDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
        public DbSet<ProjectsModel> Projects { get; set; }
        public DbSet<TechnologiesModel> Technologies { get; set; }
        public DbSet<WorkExperienceModel> WorkExperiesnces { get; set; }

    }
}
