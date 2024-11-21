using Microsoft.EntityFrameworkCore;
using ChessMaze;

namespace ChessMaze
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movement> Movements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=ChessMazeDb;Trusted_Connection=True;TrustServerCertificate=True;");


        }
    }
}
