namespace backend.Data;


using Microsoft.EntityFrameworkCore;
using backend.Models;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
