using Microsoft.EntityFrameworkCore;
using Jenkins.Models;

namespace Jenkins.Db
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
