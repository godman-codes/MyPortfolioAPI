using Entities.Models;
using Entities.SystemModels;
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
            //modelBuilder.Entity<UserModel>()
            //    .Has
        }
        public DbSet<ProjectsModel> Projects { get; set; }
        public DbSet<TechnologiesModel> Technologies { get; set; }
        public DbSet<WorkExperienceModel> WorkExperiesnces { get; set; }
        public DbSet<EmailModel> EmailLogs { get; set; }
        public DbSet<EmailTemplateModel> EmailTemplates { get; set; }


    }
}
