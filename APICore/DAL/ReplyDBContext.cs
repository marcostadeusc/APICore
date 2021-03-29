

using APICore.Models;
using Microsoft.EntityFrameworkCore;

namespace APICore.DAL
{
    public class ReplyDBContext : DbContext
    {
        public ReplyDBContext(DbContextOptions<ReplyDBContext> options) : base(options) { }
        public DbSet<EmployeesModel> Employees { get; set; }
    }
}
