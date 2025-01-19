using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    
    public DbSet<ProductParam> ProductParams { get; set; }
    
    public DbSet<User> User { get; set; }
    
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<UserRole> UserRole { get; set; }
    
    public DbSet<FileRecord> FileRecords { get; set; }
}