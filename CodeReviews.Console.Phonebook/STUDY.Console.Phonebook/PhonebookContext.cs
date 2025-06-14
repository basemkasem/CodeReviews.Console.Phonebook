using Microsoft.EntityFrameworkCore;
using STUDY.Console.Phonebook.Models;

namespace STUDY.Console.Phonebook;

public class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer("Server=(localdb)\\MyLocalDB;Database=Phonebook;Trusted_Connection=True;");
}