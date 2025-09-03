using Microsoft.EntityFrameworkCore;
using StudentRegistration.Models.Login;

namespace StudentRegistration.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor to pass options to the base class (DbContext)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties represent tables in your database
        public DbSet<StudentModel> Student { get; set; }
        // You can define additional DbSets for other entities if needed
        public DbSet<User> Users { get; set; }

        // You can add more DbSets for other entities (tables) here
    }
}
