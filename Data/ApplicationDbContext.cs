using Microsoft.EntityFrameworkCore;
using student_management_api.Models;

namespace student_management_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<StudentModel> Students { get; set; }
    }
}
