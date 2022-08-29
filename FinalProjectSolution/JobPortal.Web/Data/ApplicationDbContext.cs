using JobPortal.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace JobPortal.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<JobCategory> JobCategories { get; set; }

        public DbSet<JobDetail> JobDetails { get; set; }

        public DbSet<CandidateDetail> CandidateDetails { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }

    }
}
