using Demoapi.Models;
using Microsoft.EntityFrameworkCore;

namespace Demoapi.Context
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
        }
        public DbSet<DemoModel> DemoModels { get; set; }    
    }
}
