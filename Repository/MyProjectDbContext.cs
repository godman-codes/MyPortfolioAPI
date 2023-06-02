using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class MyProjectDbContext : IdentityDbContext<UserModel>
    {

        public MyProjectDbContext(DbContextOptions<MyProjectDbContext> options) : base(options)
        {
           
        }
        public DbSet<ProjectsModel> Projects { get; set; }
        public DbSet<TechnologiesModel> Technologies { get; set; }
        public DbSet<WorkExperienceModel> WorkExperiesnces { get; set; }
        public DbSet<ProjectTechnologiesModel> ProjectTechnologies { get; set; }
    }
}
