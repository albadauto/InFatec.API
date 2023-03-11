using Microsoft.EntityFrameworkCore;

namespace InFatec.API.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) {}

    }
}
