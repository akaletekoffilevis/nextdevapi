using Microsoft.EntityFrameworkCore;
namespace NextDevAsp.Api.DataContext
{
    public class TeamDbContext : DbContext
    {
        public TeamDbContext(DbContextOptions<TeamDbContext> options) : base(options)
        {
            
        }
        public DbSet<TeamMember> TeamMembers { get; set; }
    }
}



