using Microsoft.EntityFrameworkCore;
using STUDY.ConsoleApp.Phonebook.Models;

namespace STUDY.ConsoleApp.Phonebook;

public class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer("Server=(localdb)\\MyLocalDB;Database=Phonebook;Trusted_Connection=True;");
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Contact>()
            .HasKey(c => c.Id);
        
        builder.Entity<Contact>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Entity<Contact>()
            .Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);

        builder.Entity<Contact>()
            .Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(50);
    }
}