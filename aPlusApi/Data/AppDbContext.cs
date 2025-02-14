using Microsoft.EntityFrameworkCore;
using aPlusApi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Student { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasKey(s => s.Id); // Confirma que Id é chave primária
        modelBuilder.Entity<Student>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd(); // Indica que o valor será gerado pelo banco
        modelBuilder.Entity<Student>().ToTable("students");
    }
}