using Microsoft.EntityFrameworkCore;
using STUDY.ConsoleApp.Phonebook.Models;

namespace STUDY.ConsoleApp.Phonebook;

public class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ContactCategory> ContactCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer("Server=(localdb)\\MyLocalDB;Database=Phonebook;Trusted_Connection=True;");
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ContactCategory>()
            .HasKey(c => new {c.ContactId, c.CategoryId });
        
        builder.Entity<ContactCategory>()
            .HasOne(cc => cc.Contact)
            .WithMany(c => c.ContactCategories)
            .HasForeignKey(cc => cc.ContactId);
        
        builder.Entity<ContactCategory>()
            .HasOne(cc => cc.Category)
            .WithMany(c => c.ContactCategories)
            .HasForeignKey(cc => cc.CategoryId);
        
        builder.Entity<Contact>()
            .HasKey(c => c.ContactId);
        
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
        
        builder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        builder.Entity<Category>()
            .HasData(
                new Category
                {
                    CategoryId = 1, 
                    Name = "Family"
                }, 
                new Category
                {
                    CategoryId = 2,
                    Name = "Friends"
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Work"
                });
    }
}