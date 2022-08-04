using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XplicityHRplatformBackEnd.Models;

namespace XplicityHRplatformBackEnd.DB
{
    public class HRplatformDbContext : IdentityDbContext<User>
    {
        public HRplatformDbContext(DbContextOptions<HRplatformDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }

        public DbSet<Candidate> candidates { get; set; }
    }
}
    