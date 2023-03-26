using InFatec.API.Model;
using Microsoft.EntityFrameworkCore;

namespace InFatec.API.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) {}
        public DbSet<Login> Login { get; set; }
        public DbSet<Code> Code { get; set; }  
        public DbSet<Warnings> Warnings { get; set; }
        public DbSet<TimeLine> TimeLine { get; set; }
        public DbSet<Events> Events { get; set; }
    }
}
