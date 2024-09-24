using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;  // Ensure the namespace matches your project structure

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Optional: You can configure further settings for your models here
        modelBuilder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasColumnType("decimal(18, 2)");
    }
}
