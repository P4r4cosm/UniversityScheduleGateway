using Microsoft.EntityFrameworkCore;
using University_Schedule_Gateway.Models;

namespace University_Schedule_Gateway.Data;

public class AuthenticationContext: DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
    {
        Database.EnsureCreated(); // создаем базу данных при первом обращении
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Настройка уникального индекса для Username
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Name)
            .IsUnique();
    }
}